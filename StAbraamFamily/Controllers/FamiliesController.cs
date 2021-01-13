using System;
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
    [Authorize]
    public class FamiliesController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();
        public ActionResult Index()
        {
            var families = db.Families.Include(f => f.EvaluationLevel).Include(f => f.Father).Include(f => f.Person)
                .Include(f => f.Person1).Include(f => f.Servant).Where(x => x.IsActive == true);
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
            ResetData();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Family family)
        {
            if (ModelState.IsValid)
            {
                var fatherName = Request.Form["FatherID"].ToString() == null ? "" : Request.Form["FatherID"].ToString();
                var motherName = Request.Form["MotherID"].ToString() == null ? "" : Request.Form["MotherID"].ToString();
                var MissingPriestName = Request.Form["MissingPriestID"].ToString() == null ? "" : Request.Form["MissingPriestID"].ToString();
                var servantName = Request.Form["ServantID"].ToString() == null ? "" : Request.Form["ServantID"].ToString();
                var Evaluation = Request.Form["EvaluationLevelID"].ToString() == null ? "" : Request.Form["EvaluationLevelID"].ToString();

                if (String.IsNullOrEmpty(fatherName))
                    family.FatherID = null;
                else
                    family.FatherID = Convert.ToInt32(fatherName);


                if (String.IsNullOrEmpty(motherName))
                    family.MotherID = null;
                else
                    family.MotherID = Convert.ToInt32(motherName);

                if (String.IsNullOrEmpty(MissingPriestName))
                    family.MissingPriestID = null;
                else
                    family.MissingPriestID = Convert.ToInt32(MissingPriestName);

                if (String.IsNullOrEmpty(servantName))
                    family.ServantID = null;
                else
                    family.ServantID = Convert.ToInt32(servantName);

                if (String.IsNullOrEmpty(Evaluation))
                    family.EvaluationLevelID = null;
                else
                    family.EvaluationLevelID = Convert.ToInt32(Evaluation);

                family.IsActive = true;
                db.Families.Add(family);
                db.SaveChanges();

                var familyData = db.Families.Where(x => x.FatherID == family.FatherID).FirstOrDefault();
                var familyFather = db.People.Where(x => x.ID == family.FatherID).FirstOrDefault();
                var familyMother = db.People.Where(x => x.ID == family.MotherID).FirstOrDefault();
                if(familyFather != null)
                    familyFather.FamilyID = familyData.ID;

                if(familyMother != null)
                    familyMother.FamilyID = familyData.ID;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ResetData();
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

            ResetData();
            return View(family);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Family family)
        {
            if (ModelState.IsValid)
            {
                var fatherName = Request.Form["FatherID"].ToString() == null ? "" : Request.Form["FatherID"].ToString();
                var motherName = Request.Form["MotherID"].ToString() == null ? "" : Request.Form["MotherID"].ToString();
                var MissingPriestName = Request.Form["MissingPriestID"].ToString() == null ? "" : Request.Form["MissingPriestID"].ToString();
                var servantName = Request.Form["ServantID"].ToString() == null ? "" : Request.Form["ServantID"].ToString();
                var Evaluation = Request.Form["EvaluationLevelID"].ToString() == null ? "" : Request.Form["EvaluationLevelID"].ToString();

                if (String.IsNullOrEmpty(fatherName))
                    family.FatherID = null;
                else
                    family.FatherID = Convert.ToInt32(fatherName);


                if (String.IsNullOrEmpty(motherName))
                    family.MotherID = null;
                else
                    family.MotherID = Convert.ToInt32(motherName);

                if (String.IsNullOrEmpty(MissingPriestName))
                    family.MissingPriestID = null;
                else
                    family.MissingPriestID = Convert.ToInt32(MissingPriestName);

                if (String.IsNullOrEmpty(servantName))
                    family.ServantID = null;
                else
                    family.ServantID = Convert.ToInt32(servantName);

                if (String.IsNullOrEmpty(Evaluation))
                    family.EvaluationLevelID = null;
                else
                    family.EvaluationLevelID = Convert.ToInt32(Evaluation);

                family.IsActive = true;
                db.Entry(family).State = EntityState.Modified;
                db.SaveChanges();

                var familyData = db.Families.Where(x => x.FatherID == family.FatherID).FirstOrDefault();
                var familyFather = db.People.Where(x => x.ID == family.FatherID).FirstOrDefault();
                var familyMother = db.People.Where(x => x.ID == family.MotherID).FirstOrDefault();
                if (familyFather != null)
                    familyFather.FamilyID = familyData.ID;

                if (familyMother != null)
                    familyMother.FamilyID = familyData.ID;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ResetData();

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
            //db.Families.Remove(family);
            family.IsActive = false;
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


        public void ResetData()
        {
            ViewBag.EvaluationLevelID = new SelectList(db.EvaluationLevels, "ID", "EvaluationLevel1");
            ViewBag.MissingPriestID = new SelectList(db.Fathers.Where(x => x.IsActive == true), "ID", "FatherName");
            ViewBag.FatherID = new SelectList(db.People.Where(x => x.Gender == true && x.IsActive == true), "ID", "FullName");
            ViewBag.MotherID = new SelectList(db.People.Where(x => x.Gender == false && x.IsActive == true), "ID", "FullName");
            ViewBag.ServantID = new SelectList(db.Servants.Where(x => x.IsActive == true), "ID", "ServantName");
            ViewBag.ConfessionFather = new SelectList(db.Fathers.Where(x => x.IsActive == true), "ID", "FatherName");
        }
    }
}
