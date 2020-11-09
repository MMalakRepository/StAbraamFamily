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

        // GET: ServiceActions
        public ActionResult Index()
        {
            var serviceActions = db.ServiceActions.Include(s => s.AspNetUser).Include(s => s.Clinic).Include(s => s.Family).Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant).Include(s => s.ServiceType);
            return View(serviceActions.ToList());
        }

        // GET: ServiceActions/Details/5
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

        // GET: ServiceActions/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName");
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode");
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName");
            ViewBag.PersonID = new SelectList(db.People, "ID", "FirstName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes, "ID", "ActionType");
            return View();
        }

        // POST: ServiceActions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ServantID,UserID,ActionDate,EnterDate,Notes,ActionTypeID,IsActive,FamilyID,PersonID,HospitalID,ClinicID")] ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                db.ServiceActions.Add(serviceAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", serviceAction.UserID);
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName", serviceAction.ClinicID);
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", serviceAction.FamilyID);
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName", serviceAction.HospitalID);
            ViewBag.PersonID = new SelectList(db.People, "ID", "FirstName", serviceAction.PersonID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", serviceAction.ServantID);
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes, "ID", "ActionType", serviceAction.ActionTypeID);
            return View(serviceAction);
        }

        // GET: ServiceActions/Edit/5
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
            ViewBag.PersonID = new SelectList(db.People, "ID", "FirstName", serviceAction.PersonID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", serviceAction.ServantID);
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes, "ID", "ActionType", serviceAction.ActionTypeID);
            return View(serviceAction);
        }

        // POST: ServiceActions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ServantID,UserID,ActionDate,EnterDate,Notes,ActionTypeID,IsActive,FamilyID,PersonID,HospitalID,ClinicID")] ServiceAction serviceAction)
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
            ViewBag.PersonID = new SelectList(db.People, "ID", "FirstName", serviceAction.PersonID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", serviceAction.ServantID);
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes, "ID", "ActionType", serviceAction.ActionTypeID);
            return View(serviceAction);
        }

        // GET: ServiceActions/Delete/5
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

        // POST: ServiceActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceAction serviceAction = db.ServiceActions.Find(id);
            db.ServiceActions.Remove(serviceAction);
            db.SaveChanges();
            return RedirectToAction("Index");
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
