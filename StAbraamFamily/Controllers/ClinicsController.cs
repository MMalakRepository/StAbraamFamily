using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StAbraamFamily.Models;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Controllers
{
    [Authorize(Roles ="Management,Health")]
    public class ClinicsController : Controller
    {
        private readonly IUnitOfWork saintUnits;
        public ClinicsController(IUnitOfWork SaintUnits)
        {
            saintUnits = SaintUnits;
        }
        public ActionResult Index()
        {
            return View(saintUnits.Clinics.GetAll().Where(x => x.IsActive == true).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = saintUnits.Clinics.Get(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }
 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                clinic.IsActive = true;
                saintUnits.Clinics.Add(clinic);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            return View(clinic);
        }

         public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = saintUnits.Clinics.Get(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                saintUnits.Clinics.Update(clinic);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }
            return View(clinic);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = saintUnits.Clinics.Get(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            Clinic clinic = saintUnits.Clinics.Get(id);
            saintUnits.Clinics.Remove(clinic);
            saintUnits.Complete();
            return Json(data: new { success = true, message = "Clinic has been deleted successfully" }, JsonRequestBehavior.AllowGet);
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
