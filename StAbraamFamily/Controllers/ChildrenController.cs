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
    public class ChildrenController : Controller
    {
        private readonly IUnitOfWork saintUnits;

        public ChildrenController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            var children = saintUnits.Children.GetChildrenData();
            return View(children.ToList());
        }

        public ActionResult GetChildrenByFamily(int FamilyID)
        {
            var children = saintUnits.Children.GetChildrenDataByFamilyID(FamilyID);
            return View("Index", children.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = saintUnits.Children.Get(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        public ActionResult Create()
        {
            ViewBag.FamilyID = new SelectList(saintUnits.Families.Find(x => x.IsActive == true), "ID", "FamilyCode");
            ViewBag.ConfessionFather = new SelectList(saintUnits.Fathers.Find(x => x.IsActive == true), "ID", "FatherName");
            ViewBag.ServantID = new SelectList(saintUnits.Servants.Find(x => x.IsActive == true), "ID", "ServantName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Child child)
        {
            if (ModelState.IsValid)
            {
                child.EnteredBy = User.Identity.Name;
                child.FamilyID = Convert.ToInt32(Request.Form["FamilyID"].ToString());
                child.ConfessionFather = Convert.ToInt32(Request.Form["ConfessionFather"].ToString());
                child.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                child.IsStudying = Convert.ToBoolean(Request.Form["IsStudying"].ToString());
                //child.IsWorking = Convert.ToBoolean(Request.Form["IsWorking"].ToString());
                child.IsActive = true;
                child.EntryDate = DateTime.Now;
                saintUnits.Children.Add(child);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            ViewBag.FamilyID = new SelectList(saintUnits.Families.Find(x => x.IsActive == true), "ID", "FamilyCode", child.FamilyID);
            ViewBag.ConfessionFather = new SelectList(saintUnits.Fathers.Find(x => x.IsActive == true), "ID", "FatherName", child.ConfessionFather);
            ViewBag.ServantID = new SelectList(saintUnits.Servants.Find(x => x.IsActive == true), "ID", "ServantName", child.ServantID);
            return View(child);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = saintUnits.Children.Get(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ResetData(child);
            return View(child);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Child child)
        {
            if (ModelState.IsValid)
            {
                child.FamilyID = Convert.ToInt32(Request.Form["FamilyID"].ToString());
                child.ConfessionFather = Convert.ToInt32(Request.Form["ConfessionFather"].ToString());
                child.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                 child.IsActive = true;
                child.EntryDate = DateTime.Now;
                saintUnits.Children.Update(child);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }
            ResetData(child);
            return View(child);
        }

        private void ResetData(Child child)
        {
            ViewBag.FamilyID = new SelectList(saintUnits.Families.Find(x => x.IsActive == true), "ID", "FamilyCode", child.FamilyID);
            ViewBag.ConfessionFather = new SelectList(saintUnits.Fathers.Find(x => x.IsActive == true), "ID", "FatherName", child.ConfessionFather);
            ViewBag.ServantID = new SelectList(saintUnits.Servants.Find(x => x.IsActive == true), "ID", "ServantName", child.ServantID);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            Child child = saintUnits.Children.Get(id);
            saintUnits.Children.Remove(child);
            saintUnits.Complete();
            return Json(data: new { success = true, message = "Child has been deleted successfully" }, JsonRequestBehavior.AllowGet);
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
