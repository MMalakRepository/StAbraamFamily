using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StAbraamFamily.Models;
using StAbraamFamily.ViewModels;

namespace StAbraamFamily.Controllers
{
    public class PeopleController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

 
        public ActionResult Index()
        {
            var people = db.People.Include(p => p.Father).Include(p => p.Servant).Where(x => x.IsActive == true);
            return View(people.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

         public ActionResult Create()
        {
            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {

            if (ModelState.IsValid)
            {
                person.IsActive = true;
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName", person.ConfessionFather);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", person.ServantID);
            return View(person);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName", person.ConfessionFather);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", person.ServantID);
            return View(person);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult DeleteAction(int ID)
        {
            Person person = db.People.Find(ID);
            person.IsActive = false;
            db.SaveChanges();
            return Json(data: new { success = true, message = "Person has been deleted successfully" }, JsonRequestBehavior.AllowGet);
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
