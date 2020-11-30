using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StAbraamFamily.Models;

namespace StAbraamFamily.Controllers
{
    public class ServiceActionsController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        public ActionResult Index()
        {
            var serviceActions = db.ServiceActions.Include(s => s.AspNetUser).Include(s => s.Clinic).Include(s => s.Family).Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant).Include(s => s.ServiceType).Where(x => x.IsActive == true);
            return View(serviceActions.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceAction serviceAction = db.ServiceActions.Find(id);
            if (serviceAction == null)
            {
                return HttpNotFound();
            }
            return View(serviceAction);
        }

        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName");
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode");
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName");
            ViewBag.PersonID = new SelectList(db.People, "ID", "FullName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes, "ID", "ActionType");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                serviceAction.IsActive = true;
                db.ServiceActions.Add(serviceAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", serviceAction.UserID);
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName", serviceAction.ClinicID);
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", serviceAction.FamilyID);
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName", serviceAction.HospitalID);
            ViewBag.PersonID = new SelectList(db.People, "ID", "FullName", serviceAction.PersonID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", serviceAction.ServantID);
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes.Where(x => x.ID != 1 || x.ID != 2), "ID", "ActionType", serviceAction.ActionTypeID);
            return View(serviceAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddHealthyService(ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                serviceAction.ActionTypeID = 1;
                db.ServiceActions.Add(serviceAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", serviceAction.UserID);
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName", serviceAction.ClinicID);
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", serviceAction.FamilyID);
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName", serviceAction.HospitalID);
            ViewBag.PersonID = new SelectList(db.People, "ID", "FullName", serviceAction.PersonID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", serviceAction.ServantID);
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes.Where(x => x.ID == 2), "ID", "ActionType", serviceAction.ActionTypeID);
            return View(serviceAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBagService(ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                serviceAction.ActionTypeID = 2;
                db.ServiceActions.Add(serviceAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", serviceAction.UserID);
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName", serviceAction.ClinicID);
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", serviceAction.FamilyID);
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName", serviceAction.HospitalID);
            ViewBag.PersonID = new SelectList(db.People, "ID", "FullName", serviceAction.PersonID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", serviceAction.ServantID);
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes, "ID", "ActionType", serviceAction.ActionTypeID);
            return View(serviceAction);
        }

        public ActionResult AddHealthyService()
        {
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName");
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode");
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName");
            ViewBag.PersonID = new SelectList(db.People, "ID", "FullName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes, "ID", "ActionType");
            return View();
        }

        public ActionResult AddBagService()
        {
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName");
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode");
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName");
            ViewBag.PersonID = new SelectList(db.People, "ID", "FullName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes, "ID", "ActionType");
            return View();
        }
 
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceAction serviceAction = db.ServiceActions.Find(id);
            if (serviceAction == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", serviceAction.UserID);
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName", serviceAction.ClinicID);
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", serviceAction.FamilyID);
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName", serviceAction.HospitalID);
            ViewBag.PersonID = new SelectList(db.People, "ID", "FullName", serviceAction.PersonID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", serviceAction.ServantID);
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes, "ID", "ActionType", serviceAction.ActionTypeID);
            return View(serviceAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceAction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", serviceAction.UserID);
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName", serviceAction.ClinicID);
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", serviceAction.FamilyID);
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName", serviceAction.HospitalID);
            ViewBag.PersonID = new SelectList(db.People, "ID", "FullName", serviceAction.PersonID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", serviceAction.ServantID);
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes, "ID", "ActionType", serviceAction.ActionTypeID);
            return View(serviceAction);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceAction serviceAction = db.ServiceActions.Find(id);
            if (serviceAction == null)
            {
                return HttpNotFound();
            }
            return View(serviceAction);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            ServiceAction serviceAction = db.ServiceActions.Find(id);
            serviceAction.IsActive = false;
            db.SaveChanges();
            return Json(data: new { success = true, message = "Service has been deleted successfully" }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
