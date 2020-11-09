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
    public class ChildrenController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        // GET: Children
        public ActionResult Index()
        {
            var children = db.Children.Include(c => c.Family).Include(c => c.Father).Include(c => c.Servant);
            return View(children.ToList());
        }

        // GET: Children/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // GET: Children/Create
        public ActionResult Create()
        {
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode");
            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,DateOfBirth,IsActive,IsWorking,WorkingPlace,IsStudying,EntryDate,EnteredBy,FamilyID,ConfessionFather,ServantID")] Child child)
        {
            if (ModelState.IsValid)
            {
                db.Children.Add(child);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", child.FamilyID);
            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName", child.ConfessionFather);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", child.ServantID);
            return View(child);
        }

        // GET: Children/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", child.FamilyID);
            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName", child.ConfessionFather);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", child.ServantID);
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,DateOfBirth,IsActive,IsWorking,WorkingPlace,IsStudying,EntryDate,EnteredBy,FamilyID,ConfessionFather,ServantID")] Child child)
        {
            if (ModelState.IsValid)
            {
                db.Entry(child).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode", child.FamilyID);
            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName", child.ConfessionFather);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", child.ServantID);
            return View(child);
        }

        // GET: Children/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Child child = db.Children.Find(id);
            db.Children.Remove(child);
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
