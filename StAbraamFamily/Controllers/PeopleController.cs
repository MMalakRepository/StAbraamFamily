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
    public class PeopleController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        // GET: People
        public ActionResult Index()
        {
            var people = db.People.Include(p => p.Father).Include(p => p.Servant);
            return View(people.ToList());
        }

        // GET: People/Details/5
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

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Phone,Address,Area,DateOfBirth,DateOfDeath,WorkingPlace,IsActive,Description,FamilyID,EntryDate,ServantID,ConfessionFather,IsWidow,Gender,Notes,NationalID,Salary,OtherIncome")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName", person.ConfessionFather);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", person.ServantID);
            return View(person);
        }

        // GET: People/Edit/5
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

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Phone,Address,Area,DateOfBirth,DateOfDeath,WorkingPlace,IsActive,Description,FamilyID,EntryDate,ServantID,ConfessionFather,IsWidow,Gender,Notes,NationalID,Salary,OtherIncome")] Person person)
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

        // GET: People/Delete/5
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

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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
