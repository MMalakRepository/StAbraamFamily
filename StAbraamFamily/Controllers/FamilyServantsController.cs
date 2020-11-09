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
    public class FamilyServantsController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        // GET: FamilyServants
        public ActionResult Index()
        {
            var familyServants = db.FamilyServants.Include(f => f.Family).Include(f => f.Servant);
            return View(familyServants.ToList());
        }

        // GET: FamilyServants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyServant familyServant = db.FamilyServants.Find(id);
            if (familyServant == null)
            {
                return HttpNotFound();
            }
            return View(familyServant);
        }

        // GET: FamilyServants/Create
        public ActionResult Create()
        {
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            return View();
        }

        // POST: FamilyServants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FamilyID,ServantID,IsActive")] FamilyServant familyServant)
        {
            if (ModelState.IsValid)
            {
                db.FamilyServants.Add(familyServant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", familyServant.FamilyID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", familyServant.ServantID);
            return View(familyServant);
        }

        // GET: FamilyServants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyServant familyServant = db.FamilyServants.Find(id);
            if (familyServant == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", familyServant.FamilyID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", familyServant.ServantID);
            return View(familyServant);
        }

        // POST: FamilyServants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FamilyID,ServantID,IsActive")] FamilyServant familyServant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familyServant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", familyServant.FamilyID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", familyServant.ServantID);
            return View(familyServant);
        }

        // GET: FamilyServants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyServant familyServant = db.FamilyServants.Find(id);
            if (familyServant == null)
            {
                return HttpNotFound();
            }
            return View(familyServant);
        }

        // POST: FamilyServants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FamilyServant familyServant = db.FamilyServants.Find(id);
            db.FamilyServants.Remove(familyServant);
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
