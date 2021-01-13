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

namespace StAbraamFamily.Controllers
{
    [Authorize]
    public class FamilyServantsController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        public ActionResult Index()
        {
            var familyServants = db.FamilyServants.Include(f => f.Family).Include(f => f.Servant).Where(x =>x.IsActive ==true);
            return View(familyServants.ToList());
        }

 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyServant familyServant = db.FamilyServants.Find(id);
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
            ViewBag.FamilyID = new SelectList(db.Families.Where(x => x.IsActive == true), "ID", "FamilyCode");
            ViewBag.ServantID = new SelectList(db.Servants.Where(x => x.IsActive == true), "ID", "ServantName");
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
                db.FamilyServants.Add(familyServant);
                db.SaveChanges();
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
            FamilyServant familyServant = db.FamilyServants.Find(id);
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
                db.Entry(familyServant).State = EntityState.Modified;
                db.SaveChanges();
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
            FamilyServant familyServant = db.FamilyServants.Find(id);
            if (familyServant == null)
            {
                return HttpNotFound();
            }
            return View(familyServant);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            FamilyServant familyServant = db.FamilyServants.Find(id);
            familyServant.IsActive = false;
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
