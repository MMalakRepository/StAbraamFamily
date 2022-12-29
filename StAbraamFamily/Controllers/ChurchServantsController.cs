using StAbraamFamily.Models;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StAbraamFamily.Controllers
{
    public class ChurchServantsController : Controller
    {
        private readonly IUnitOfWork saintUnits;
        public ChurchServantsController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            return View(saintUnits.ChurchServants.Find(x => x.IsActive == true).ToList());
        }

        [HttpGet]
        [Route("ChurchServants")]
        public ActionResult NewServant()
        {
            var services = saintUnits.ChurchServices.GetAll();
            ViewBag.Services = new SelectList(services, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ChurchServants")]
        public ActionResult NewServant(ServantModel model)
        {
            var existing = saintUnits.ChurchServants.Find(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefault();
            if (existing != null)
            {
                ModelState.AddModelError("FullName", "تم أدخال  رقم الهاتف من قبل");
                ViewBag.Message = "تم أدخال  رقم الهاتف من قبل";
            }

            existing = saintUnits.ChurchServants.Find(x => x.Name == model.Name).FirstOrDefault();
            if (existing != null)
            {
                ModelState.AddModelError("FullName", "تم أدخال هذا الأسم من قبل");
                ViewBag.Message = "تم أدخال هذا الأسم من قبل";
            }

            if (ModelState.IsValid)
            {
                var servant = new ChurchServant()
                {
                    Name = model.Name,
                    ConfessionFR = model.ConfessionFR,
                    ConfessionFRChurch = model.ConfessionFRChurch,
                    ConfessionFRNumber = model.ConfessionFRNumber,
                    EmailAddress = model.EmailAddress,
                    Job = model.Job,
                    PhoneNumber = model.PhoneNumber,
                    PreviousServices = model.PreviousServices,
                    Readings = model.Readings,
                    SpecialStudies = model.SpecialStudies,
                    Studying = model.Studying,
                    IsActive = true,
                    CreatedOn = DateTime.Now
                };
                servant.ServantServices = new List<ServantService>();
                List<string> services = new List<string>();
                foreach (var service in model.Services)
                {
                    var t = Convert.ToInt32(service);
                    var s = saintUnits.ChurchServices.Find(x => x.ID == t).FirstOrDefault().Name;
                    services.Add(s);
                    servant.ServantServices.Add(new ServantService() { ChurchServiceID = t });
                }

                saintUnits.ChurchServants.Add(servant);
                saintUnits.Complete();

                model.ServiceName = String.Join(",", services.ToArray());
                //model.ServiceName = saintUnits.ChurchServices.Find(x => x.ID == model.ServiceID).FirstOrDefault().Name;
                return View("GetServantDetails", model);
            }
            return View(model);
        }
    }
}