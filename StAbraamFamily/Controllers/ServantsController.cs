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
    [Authorize(Roles = "Management,DataEntry")]
    public class ServantsController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        public ActionResult Index()
        {
            return View(db.Servants.Where(x => x.IsActive == true).ToList());
        }

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
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Servant servant)
        {
            if (ModelState.IsValid)
            {
                servant.IsActive = true;
                db.Entry(servant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servant);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            Servant servant = db.Servants.Find(id);
            servant.IsActive = false;
            db.SaveChanges();
            return Json(data: new { sucess = true, message = "Servant has been deleted successfully" }, JsonRequestBehavior.AllowGet);
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
