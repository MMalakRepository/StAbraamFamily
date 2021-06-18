using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Controllers
{
    [Authorize(Roles = "Management")]
    public class ServiceTypesController : Controller
    {
        private readonly IUnitOfWork saintUnits;
        public ServiceTypesController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            return View(saintUnits.ServiceTypes.Find(x => x.IsActive ==true).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.ServantID = new SelectList(saintUnits.Servants.Find(x => x.IsActive == true), "ID", "ServantName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                serviceType.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                serviceType.IsActive = true;
                saintUnits.ServiceTypes.Add(serviceType);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            return View(serviceType);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = saintUnits.ServiceTypes.Get(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                serviceType.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                saintUnits.ServiceTypes.Update(serviceType);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }
            return View(serviceType);
        }

         [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            ServiceType serviceType = saintUnits.ServiceTypes.Get(id);
            saintUnits.ServiceTypes.Remove(serviceType);
            saintUnits.Complete();
            return Json(data: new { success = true, message = "Service Type Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                saintUnits.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
