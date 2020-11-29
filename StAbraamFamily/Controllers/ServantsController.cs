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
    public class ServantsController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        // GET: Servants
        public ActionResult Index()
        {
            return View(db.Servants.ToList());
        }

        // GET: Servants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servant servant = db.Servants.Find(id);
            if (servant == null)
            {
                return HttpNotFound();
            }
            return View(servant);
        }

        // GET: Servants/Create
        public ActionResult Create()
        {
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Servant servant)
        {
            if (ModelState.IsValid)
            {
                servant.IsActive = true;
                db.Servants.Add(servant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servant);
        }

        // GET: Servants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servant servant = db.Servants.Find(id);
            if (servant == null)
            {
                return HttpNotFound();
            }
            return View(servant);
        }

        // POST: Servants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ServantName,Phone,EmailAddress,DateOfBirth,IsActive")] Servant servant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servant);
        }

        // GET: Servants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servant servant = db.Servants.Find(id);
            if (servant == null)
            {
                return HttpNotFound();
            }
            return View(servant);
        }

        // POST: Servants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servant servant = db.Servants.Find(id);
            db.Servants.Remove(servant);
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
