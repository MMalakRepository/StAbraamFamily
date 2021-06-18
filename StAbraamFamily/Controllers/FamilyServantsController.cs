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
    public class FamilyServantsController : Controller
    { 
        private readonly IUnitOfWork saintUnits;

        public FamilyServantsController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            var familyServants = saintUnits.FamilyServants.GetAll().Where(x =>x.IsActive ==true);
            return View(familyServants.ToList());
        }

 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyServant familyServant = saintUnits.FamilyServants.Get(id);
            if (familyServant == null)
            {
                return HttpNotFound();
            }
            return View(familyServant);
        }

 
        public ActionResult Create()
        {
            ResetData();
            return View();
        }
 

        public void ResetData()
        {
            ViewBag.FamilyID = new SelectList(saintUnits.Families.GetAll().Where(x => x.IsActive == true), "ID", "FamilyCode");
            ViewBag.ServantID = new SelectList(saintUnits.Servants.GetAll().Where(x => x.IsActive == true), "ID", "ServantName");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FamilyServant familyServant)
        {
            if (ModelState.IsValid)
            {

                familyServant.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                familyServant.FamilyID = Convert.ToInt32(Request.Form["FamilyID"].ToString());
                familyServant.IsActive = true;
                saintUnits.FamilyServants.Add(familyServant);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            ResetData();
            return View(familyServant);
        }

        // GET: FamilyServants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyServant familyServant = saintUnits.FamilyServants.Get(id);
            if (familyServant == null)
            {
                return HttpNotFound();
            }
            ResetData();
            return View(familyServant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FamilyServant familyServant)
        {
            if (ModelState.IsValid)
            {
                familyServant.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                familyServant.FamilyID = Convert.ToInt32(Request.Form["FamilyID"].ToString());
                familyServant.IsActive = true;
                saintUnits.FamilyServants.Update(familyServant);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            ResetData();
            return View(familyServant);
        }

        // GET: FamilyServants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyServant familyServant = saintUnits.FamilyServants.Get(id);
            if (familyServant == null)
            {
                return HttpNotFound();
            }
            return View(familyServant);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            FamilyServant familyServant = saintUnits.FamilyServants.Get(id);
            familyServant.IsActive = false;
            saintUnits.Complete();
            return Json(data: new { sucess = true, message = "Servant has been deleted successfully" }, JsonRequestBehavior.AllowGet);
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
