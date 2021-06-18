using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StAbraamFamily.Models;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Controllers
{
    [Authorize(Roles = "Management,DataEntry")]
    public class ServantsController : Controller
    {
        private readonly IUnitOfWork saintUnits;
        public ServantsController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            return View(saintUnits.Servants.Find(x => x.IsActive == true).ToList());
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
                saintUnits.Servants.Add(servant);
                saintUnits.Complete();
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
            Servant servant = saintUnits.Servants.Get(id);
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
                saintUnits.Servants.Update(servant);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }
            return View(servant);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            Servant servant = saintUnits.Servants.Get(id);
            servant.IsActive = false;
            saintUnits.Servants.Remove(servant);
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
