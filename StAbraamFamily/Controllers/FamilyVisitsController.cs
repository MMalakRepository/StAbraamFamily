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

        // GET: FamilyVisits
        public ActionResult Index()
        {
            var familyVisits = db.FamilyVisits.Include(f => f.Family).Include(f => f.Servant);
            return View(familyVisits.ToList());
        }

        // GET: FamilyVisits/Details/5
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

        // GET: FamilyVisits/Create
        public ActionResult Create()
        {
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            return View();
        }

        // POST: FamilyVisits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FamilyID,ServantID,VisitDate,Notes")] FamilyVisit familyVisit)
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

        // GET: FamilyVisits/Edit/5
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

        // POST: FamilyVisits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: FamilyVisits/Delete/5
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

        // POST: FamilyVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FamilyVisit familyVisit = db.FamilyVisits.Find(id);
            db.FamilyVisits.Remove(familyVisit);
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
