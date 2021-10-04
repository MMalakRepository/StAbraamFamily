using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StAbraamFamily.Models;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Controllers
{
    [Authorize(Roles = "Management,DataEntry")]
    public class PeopleController : Controller
    {
        private readonly IUnitOfWork saintUnits;

        public PeopleController(IUnitOfWork SaintUnits)
        {
            saintUnits = SaintUnits;
        }
         public ActionResult Index()
        {
            var people = saintUnits.People.GetPeopleData();
            return View(people);
        }
         public ActionResult Create()
        {
            ViewBag.ConfessionFather = new SelectList(saintUnits.Fathers.Find(x => x.IsActive==true), "ID", "FatherName");
            ViewBag.ServantID = new SelectList(saintUnits.Servants.Find(x => x.IsActive == true), "ID", "ServantName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            var IsExisted = saintUnits.People.Find(x => x.Code == person.Code).ToList();
            if (IsExisted.Count > 0 )
                ModelState.AddModelError(String.Empty, "تم أدخال نفس الكود مسبقاً");

            var IsNationalIDExisted = saintUnits.People.Find(x => x.NationalID == person.NationalID).ToList();
            if (IsNationalIDExisted.Count > 0)
                ModelState.AddModelError(String.Empty, "تم أدخال نفس رقم البطاقة مسبقاً");

            if (ModelState.IsValid)
            {
                person.IsActive = true;
                if(!String.IsNullOrEmpty(Request.Form["ServantID"].ToString()))
                    person.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                if(!String.IsNullOrEmpty(Request.Form["ConfessionFather"].ToString()))
                    person.ConfessionFather = Convert.ToInt32(Request.Form["ConfessionFather"].ToString());

                saintUnits.People.Add(person);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            ResetData();
            return View();
        }

        private void ResetData()
        {
            ViewBag.ConfessionFather = new SelectList(saintUnits.Fathers.Find(x => x.IsActive ==true), "ID", "FatherName");
            ViewBag.ServantID = new SelectList(saintUnits.Servants.Find(x => x.IsActive == true), "ID", "ServantName");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = saintUnits.People.Get(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            //ResetData();
            ViewBag.ConfessionFather = new SelectList(saintUnits.Fathers.Find(x => x.IsActive == true), "ID", "FatherName",saintUnits.Fathers.Find(x => x.ID == person.ConfessionFather).Select(c => c.ID));
            ViewBag.ServantID = new SelectList(saintUnits.Servants.Find(x => x.IsActive == true), "ID", "ServantName",saintUnits.Servants.Find(x => x.ID == person.ServantID).Select(c => c.ID));


            return View(person);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                person.IsActive = true;
                person.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                person.ConfessionFather = Convert.ToInt32(Request.Form["ConfessionFather"].ToString());
                saintUnits.People.Update(person);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }
            ResetData();
            return View(person);
        }

        [HttpPost]
        public ActionResult DeleteAction(int ID)
        {
            Person person = saintUnits.People.Get(ID);
            saintUnits.People.Remove(person);
            saintUnits.Complete();
            return Json(data: new { success = true, message = "Person has been deleted successfully" }, JsonRequestBehavior.AllowGet);
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
