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
    public class FamilyVisitsController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

 
        public ActionResult Index()
        {
            var familyVisits = db.FamilyVisits.Include(f => f.Family).Include(f => f.Servant);
            return View(familyVisits.ToList());
        }

 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyVisit familyVisit = db.FamilyVisits.Find(id);
            if (familyVisit == null)
            {
                return HttpNotFound();
            }
            return View(familyVisit);
        }

 
        public ActionResult Create()
        {
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FamilyVisit familyVisit)
        {
            if (ModelState.IsValid)
            {
        
                db.FamilyVisits.Add(familyVisit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", familyVisit.FamilyID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", familyVisit.ServantID);
            return View(familyVisit);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyVisit familyVisit = db.FamilyVisits.Find(id);
            if (familyVisit == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", familyVisit.FamilyID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", familyVisit.ServantID);
            return View(familyVisit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FamilyID,ServantID,VisitDate,Notes")] FamilyVisit familyVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familyVisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", familyVisit.FamilyID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", familyVisit.ServantID);
            return View(familyVisit);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyVisit familyVisit = db.FamilyVisits.Find(id);
            if (familyVisit == null)
            {
                return HttpNotFound();
            }
            return View(familyVisit);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            FamilyVisit familyVisit = db.FamilyVisits.Find(id);
            db.FamilyVisits.Remove(familyVisit);
            db.SaveChanges();
            return Json(data: new { success = true, message = "Visit has been deleted successfully" }, JsonRequestBehavior.AllowGet);
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
