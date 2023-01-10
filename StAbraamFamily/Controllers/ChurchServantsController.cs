using AutoMapper;
using StAbraamFamily.Models;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace StAbraamFamily.Controllers
{
    public class ChurchServantsController : Controller
    {
        #region ::State::
        private readonly IUnitOfWork saintUnits;
        #endregion

        #region::Constructor:;
        public ChurchServantsController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        #endregion

        #region::Report::
        [Authorize(Roles = "Management")]
        public ActionResult Servants()
        {
            var servants = saintUnits.ChurchServants.Find(x => x.IsActive == true).ToList();
            var t = servants.Select(a => new ServantModel()
            {
                ConfessionFR = a.ConfessionFR,
                ConfessionFRChurch = a.ConfessionFRChurch,
                ConfessionFRNumber = a.ConfessionFRNumber,
                EmailAddress = a.EmailAddress,
                Job = a.Job,
                Name = a.Name,
                PhoneNumber = a.PhoneNumber,
                PreviousServices = a.PreviousServices,
                Readings = a.Readings,
                SpecialStudies = a.SpecialStudies,
                Studying = a.Studying
            }).ToList();
            return View(saintUnits.ChurchServants.Find(x => x.IsActive == true).ToList());
        }
        #endregion

        [HttpGet]
        [Route("ChurchServants")]
        public ActionResult NewServant()
        {
            var services = saintUnits.ChurchServices.GetAll();
            ViewBag.Services = new SelectList(services, "ID", "Name");
            ViewBag.Error = string.Empty;
            return View();
        }

        [HttpPost]
        [Route("ChurchServants")]
        public ActionResult NewServant(ServantModel model)
        {
            try
            {
                var existing = saintUnits.ChurchServants.Find(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefault();
                if (existing == null)
                    existing = saintUnits.ChurchServants.Find(x => x.Name == model.Name).FirstOrDefault();

                if (existing != null)
                {
                    var ServantServices = new List<ServantService>();
                    List<string> services = new List<string>();

                    foreach (var service in model.Services)
                    {
                        var t = Convert.ToInt32(service);
                        var s = saintUnits.ChurchServices.Find(x => x.ID == t).FirstOrDefault();
                        if (s != null)
                            services.Add(s.Name);
                    }
                    model.ServiceName = String.Join(",", existing.ServantServices.Select(x => x.ChurchService.Name).ToList().ToArray());
                    return View("GetServantDetails", model);
                }
                else
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
                        var s = saintUnits.ChurchServices.Find(x => x.ID == t).FirstOrDefault();
                        if (s != null)
                            services.Add(s.Name);
                        servant.ServantServices.Add(new ServantService() { ChurchServiceID = t });
                    }

                    saintUnits.ChurchServants.Add(servant);
                    saintUnits.Complete();

                    model.ServiceName = String.Join(",", services.ToArray());
                    //model.ServiceName = saintUnits.ChurchServices.Find(x => x.ID == model.ServiceID).FirstOrDefault().Name;
                    return View("GetServantDetails", model);
                }

            }
            catch (Exception ex)
            {
                var services = saintUnits.ChurchServices.GetAll();
                ViewBag.Services = new SelectList(services, "ID", "Name");
                ViewBag.Error = "نأسف لهذا الخطأ برجاء المحاولة مرة أخرى او التواصل مع المسئول / " + ex.Message;
                return View("NewServant");
            }

        }

        [HttpGet]
        [Route("ServantsReport/{ID}")]
        public ActionResult ServantsList(int ID)
        {
            var servants = saintUnits.ChurchServants.GetServantsByServiceID(ID);

            List<ServantModel> servantModels = new List<ServantModel>();
            foreach (var servant in servants)
            {
                ServantModel model = new ServantModel()
                {
                    Name = servant.Name,
                    PhoneNumber = servant.PhoneNumber,
                    Job = servant.Job,
                    ConfessionFR = servant.ConfessionFR,
                    ConfessionFRChurch = servant.ConfessionFRChurch,
                    ConfessionFRNumber = servant.ConfessionFRNumber,
                    EmailAddress = servant.EmailAddress,
                    PreviousServices = servant.PreviousServices,
                    Readings = servant.Readings,
                    SpecialStudies = servant.SpecialStudies,
                    Studying = servant.Studying,
                };

                List<string> services = new List<string>();
                foreach (var service in servant.ServantServices)
                {
                    services.Add(service.ChurchService?.Name);
                }
                model.ServiceName = String.Join(",", services.ToArray());
                servantModels.Add(model);
            }
            return View(servantModels);
        }
    }
}