﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StAbraamFamily.Models;
using StAbraamFamily.UnitsOfWork;
using StAbraamFamily.ViewModels;


namespace StAbraamFamily.Controllers
{
    public class FamiliesController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();
        public ActionResult Index()
        {
            var families = db.Families.Include(f => f.EvaluationLevel).Include(f => f.Father).Include(f => f.Person).Include(f => f.Person1).Include(f => f.Servant);
            return View(families.ToList());
        }

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

        public ActionResult Create()
        {
            ViewBag.EvaluationLevelID = new SelectList(db.EvaluationLevels, "ID", "EvaluationLevel1");
            ViewBag.MissingPriestID = new SelectList(db.Fathers, "ID", "FatherName");
            ViewBag.FatherID = new SelectList(db.People.Where(x => x.Gender == true), "ID", "FullName");
            ViewBag.MotherID = new SelectList(db.People.Where(x => x.Gender == false), "ID", "FullName");
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName");
            ViewBag.ConfessionFather = new SelectList(db.Fathers, "ID", "FatherName");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Family family)
        {
            if (ModelState.IsValid)
            {
                family.FatherID = Convert.ToInt32(Request.Form["FatherID"].ToString());
                family.MotherID = Convert.ToInt32(Request.Form["MotherID"].ToString());
                family.MissingPriestID = Convert.ToInt32(Request.Form["MissingPriestID"].ToString());
                family.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                family.IsActive = true;
                db.Families.Add(family);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EvaluationLevelID = new SelectList(db.EvaluationLevels, "ID", "EvaluationLevel1", family.EvaluationLevelID);
            ViewBag.MissingPriestID = new SelectList(db.Fathers, "ID", "FatherName", family.MissingPriestID);
            ViewBag.FatherID = new SelectList(db.People.Where(x => x.Gender == true), "ID", "FullName", family.FatherID);
            ViewBag.MotherID = new SelectList(db.People.Where(x => x.Gender == false), "ID", "FullName", family.MotherID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", family.ServantID);
            return View(family);
        }
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
            ViewBag.FatherID = new SelectList(db.People.Where(x => x.Gender == true), "ID", "FullName", family.FatherID);
            ViewBag.MotherID = new SelectList(db.People.Where(x => x.Gender == false), "ID", "FullName", family.MotherID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", family.ServantID);
            return View(family);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Family family)
        {
            if (ModelState.IsValid)
            {
                family.FatherID = Convert.ToInt32(Request.Form["FatherID"].ToString());
                family.MotherID = Convert.ToInt32(Request.Form["MotherID"].ToString());
                family.MissingPriestID = Convert.ToInt32(Request.Form["MissingPriestID"].ToString());
                family.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                db.Entry(family).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EvaluationLevelID = new SelectList(db.EvaluationLevels, "ID", "EvaluationLevel1", family.EvaluationLevelID);
            ViewBag.MissingPriestID = new SelectList(db.Fathers, "ID", "FatherName", family.MissingPriestID);
            ViewBag.FatherID = new SelectList(db.People.Where(x => x.Gender == true), "ID", "FullName", family.FatherID);
            ViewBag.MotherID = new SelectList(db.People.Where(x => x.Gender == false), "ID", "FullName", family.MotherID);
            ViewBag.ServantID = new SelectList(db.Servants, "ID", "ServantName", family.ServantID);

            return View(family);
        }

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
