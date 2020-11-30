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

        public ActionResult Create()
        {
            ViewBag.FamilyID = new SelectList(db.Families, "ID", "FamilyCode");
            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Child child)
        {
            if (ModelState.IsValid)
            {
                child.EnteredBy = User.Identity.Name;
                child.IsActive = true;
                child.EntryDate = DateTime.Now;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Child child)
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

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            Child child = db.Children.Find(id);
            child.IsActive = false;
            db.SaveChanges();
            return Json(data: new { success = true, message = "Child has been deleted successfully" }, JsonRequestBehavior.AllowGet);
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
