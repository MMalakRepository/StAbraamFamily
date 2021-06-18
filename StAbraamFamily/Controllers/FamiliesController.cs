using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;


namespace StAbraamFamily.Controllers
{
    [Authorize(Roles = "Management,DataEntry")]
    public class FamiliesController : Controller
    {
        private readonly IUnitOfWork saintUnits;

        public FamiliesController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            var families = saintUnits.Families.GetAll().Where(x => x.IsActive == true);
            return View(families.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = saintUnits.Families.Get(id);
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
            var IsExisted = saintUnits.Families.GetAll().Any(x => x.FamilyCode == family.FamilyCode);
            if (IsExisted)
                ModelState.AddModelError(String.Empty, "تم أدخال نفس الكود مسبقاً");
  

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
                saintUnits.Families.Add(family);
                saintUnits.Complete();

                var familyData = saintUnits.Families.SingleOrDefault(x => x.FatherID == family.FatherID);
                var familyFather = saintUnits.People.SingleOrDefault(x => x.ID == family.FatherID);
                var familyMother = saintUnits.People.SingleOrDefault(x => x.ID == family.MotherID);
                if(familyFather != null)
                    familyFather.FamilyID = familyData.ID;

                if(familyMother != null)
                    familyMother.FamilyID = familyData.ID;

                saintUnits.Complete();
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
            Family family = saintUnits.Families.Get(id);
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
                saintUnits.Families.Update(family);
                saintUnits.Complete();

                var familyData = saintUnits.Families.SingleOrDefault(x => x.FatherID == family.FatherID);
                var familyFather = saintUnits.People.SingleOrDefault(x => x.ID == family.FatherID);
                var familyMother = saintUnits.People.SingleOrDefault(x => x.ID == family.MotherID);
                if (familyFather != null)
                    familyFather.FamilyID = familyData.ID;

                if (familyMother != null)
                    familyMother.FamilyID = familyData.ID;

                saintUnits.Complete();
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
            Family family = saintUnits.Families.Get(id);
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
            Family family = saintUnits.Families.Get(id);
            //db.Families.Remove(family);
            saintUnits.Families.Remove(family);
            saintUnits.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                saintUnits.Dispose();
            }
            base.Dispose(disposing);
        }


        public void ResetData()
        {
            ViewBag.EvaluationLevelID = new SelectList(saintUnits.EvaluationLevels.GetAll(), "ID", "EvaluationLevel1");
            ViewBag.MissingPriestID = new SelectList(saintUnits.Fathers.GetAll().Where(x => x.IsActive == true), "ID", "FatherName");
            ViewBag.FatherID = new SelectList(saintUnits.People.GetAll().Where(x => x.Gender == true && x.IsActive == true), "ID", "FullName");
            ViewBag.MotherID = new SelectList(saintUnits.People.GetAll().Where(x => x.Gender == false && x.IsActive == true), "ID", "FullName");
            ViewBag.ServantID = new SelectList(saintUnits.Servants.GetAll().Where(x => x.IsActive == true), "ID", "ServantName");
            ViewBag.ConfessionFather = new SelectList(saintUnits.Fathers.GetAll().Where(x => x.IsActive == true), "ID", "FatherName");
        }
    }
}
