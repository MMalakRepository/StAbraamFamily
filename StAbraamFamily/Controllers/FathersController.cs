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
    [Authorize]
    public class FathersController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        public ActionResult Index()
        {
            return View(db.Fathers.Where(x => x.IsActive ==true).ToList());
        }
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Father father)
        {
            if (ModelState.IsValid)
            {
                father.IsActive = true;
                db.Fathers.Add(father);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(father);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Father father = db.Fathers.Find(id);
            if (father == null)
            {
                return HttpNotFound();
            }
            return View(father);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Father father)
        {
            if (ModelState.IsValid)
            {
                db.Entry(father).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(father);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            Father father = db.Fathers.Find(id);
            father.IsActive = false;
            //db.Fathers.Remove(father);
            db.SaveChanges();
            return Json(data : new { success = true , message ="Father has been deleted successfully"},JsonRequestBehavior.AllowGet);
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
