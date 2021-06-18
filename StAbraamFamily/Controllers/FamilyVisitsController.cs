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
    [Authorize(Roles = "Management")]
    public class FamilyVisitsController : Controller
    {
        private readonly IUnitOfWork saintUnits;
        public FamilyVisitsController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            var familyVisits = saintUnits.FamilyVisits.GetAll();
            return View(familyVisits.ToList());
        }
 
        public ActionResult Create()
        {
            ResetData();
            return View();
        }

        private void ResetData()
        {
            ViewBag.FamilyID = new SelectList(saintUnits.Families.GetAll().Where(x => x.IsActive == true), "ID", "FamilyCode");
            ViewBag.ServantID = new SelectList(saintUnits.Servants.GetAll().Where(x => x.IsActive == true), "ID", "ServantName");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FamilyVisit familyVisit)
        {
            if (ModelState.IsValid)
            {
               
                familyVisit.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                familyVisit.FamilyID = Convert.ToInt32(Request.Form["FamilyID"].ToString());
                saintUnits.FamilyVisits.Add(familyVisit);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            ResetData();
            return View(familyVisit);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyVisit familyVisit = saintUnits.FamilyVisits.Get(id);
            if (familyVisit == null)
            {
                return HttpNotFound();
            }


            ResetData();
            return View(familyVisit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FamilyVisit familyVisit)
        {
            if (ModelState.IsValid)
            {

                familyVisit.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                familyVisit.FamilyID = Convert.ToInt32(Request.Form["FamilyID"].ToString());
                saintUnits.FamilyVisits.Update(familyVisit);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            ResetData();
            return View(familyVisit);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            FamilyVisit familyVisit = saintUnits.FamilyVisits.Get(id);
            saintUnits.FamilyVisits.Remove(familyVisit);
            saintUnits.Complete();
            return Json(data: new { success = true, message = "Visit has been deleted successfully" }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                saintUnits.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
