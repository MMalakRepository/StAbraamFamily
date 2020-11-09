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
    public class FamiliesController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        // GET: Families
        public ActionResult Index()
        {
            var families = db.Families.Include(f => f.EvaluationLevel).Include(f => f.Father).Include(f => f.Person).Include(f => f.Person1).Include(f => f.Servant);
            return View(families.ToList());
        }

        // GET: Families/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        // GET: Families/Create
        public ActionResult Create()
        {
            ViewBag.EvaluationLevelID = new SelectList(db.EvaluationLevels, "ID", "EvaluationLevel1");
            ViewBag.MissingPriestID = new SelectList(db.Fathers, "ID", "FatherName");
            ViewBag.FatherID = new SelectList(db.People, "ID", "FirstName");
            ViewBag.MotherID = new SelectList(db.People, "ID", "FirstName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            return View();
        }

        // POST: Families/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FamilyCode,Address,ServantID,NoOfChildren,FatherID,MotherID,IsActive,Notes,MissingPriestID,EvaluationLevelID")] Family family)
        {
            if (ModelState.IsValid)
            {
                db.Families.Add(family);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EvaluationLevelID = new SelectList(db.EvaluationLevels, "ID", "EvaluationLevel1", family.EvaluationLevelID);
            ViewBag.MissingPriestID = new SelectList(db.Fathers, "ID", "FatherName", family.MissingPriestID);
            ViewBag.FatherID = new SelectList(db.People, "ID", "FirstName", family.FatherID);
            ViewBag.MotherID = new SelectList(db.People, "ID", "FirstName", family.MotherID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", family.ServantID);
            return View(family);
        }

        // GET: Families/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            ViewBag.EvaluationLevelID = new SelectList(db.EvaluationLevels, "ID", "EvaluationLevel1", family.EvaluationLevelID);
            ViewBag.MissingPriestID = new SelectList(db.Fathers, "ID", "FatherName", family.MissingPriestID);
            ViewBag.FatherID = new SelectList(db.People, "ID", "FirstName", family.FatherID);
            ViewBag.MotherID = new SelectList(db.People, "ID", "FirstName", family.MotherID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", family.ServantID);
            return View(family);
        }

        // POST: Families/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FamilyCode,Address,ServantID,NoOfChildren,FatherID,MotherID,IsActive,Notes,MissingPriestID,EvaluationLevelID")] Family family)
        {
            if (ModelState.IsValid)
            {
                db.Entry(family).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EvaluationLevelID = new SelectList(db.EvaluationLevels, "ID", "EvaluationLevel1", family.EvaluationLevelID);
            ViewBag.MissingPriestID = new SelectList(db.Fathers, "ID", "FatherName", family.MissingPriestID);
            ViewBag.FatherID = new SelectList(db.People, "ID", "FirstName", family.FatherID);
            ViewBag.MotherID = new SelectList(db.People, "ID", "FirstName", family.MotherID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", family.ServantID);
            return View(family);
        }

        // GET: Families/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        // POST: Families/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Family family = db.Families.Find(id);
            db.Families.Remove(family);
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
