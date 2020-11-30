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
 
        public ActionResult Create()
        {
            ResetData();
            return View();
        }

        private void ResetData()
        {
            ViewBag.FamilyID = new SelectList(db.Families.Where(x => x.IsActive == true), "ID", "FamilyCode");
            ViewBag.ServantID = new SelectList(db.Servants.Where(x => x.IsActive == true), "ID", "ServantName");
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

            ResetData();
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


            ResetData();
            return View(familyVisit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FamilyVisit familyVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familyVisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ResetData();
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
