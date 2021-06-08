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
    [Authorize(Roles ="Management")]
    public class EvaluationLevelsController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        public ActionResult Index()
        {
            return View(db.EvaluationLevels.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvaluationLevel evaluationLevel)
        {
            if (ModelState.IsValid)
            {
                db.EvaluationLevels.Add(evaluationLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evaluationLevel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationLevel evaluationLevel = db.EvaluationLevels.Find(id);
            if (evaluationLevel == null)
            {
                return HttpNotFound();
            }
            return View(evaluationLevel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EvaluationLevel evaluationLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluationLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evaluationLevel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationLevel evaluationLevel = db.EvaluationLevels.Find(id);
            if (evaluationLevel == null)
            {
                return HttpNotFound();
            }
            return View(evaluationLevel);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            EvaluationLevel evaluationLevel = db.EvaluationLevels.Find(id);
            //db.EvaluationLevels.Remove(evaluationLevel);
            evaluationLevel.IsActive = false;
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
