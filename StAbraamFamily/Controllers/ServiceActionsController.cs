using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StAbraamFamily.Models;
using Microsoft.AspNet.Identity;

namespace StAbraamFamily.Controllers
{

    public class ServiceActionsController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        [Authorize(Roles = "Management")]
        public ActionResult Index()
        {
            var serviceActions = db.ServiceActions.Include(s => s.AspNetUser)
                .Include(s => s.Clinic).Include(s => s.Family)
                .Include(s => s.Hospital).Include(s => s.Person)
                .Include(s => s.Servant).Include(s => s.ServiceType)
                .Where(x => x.IsActive == true && (x.ServiceType.ID != 1 || x.ServiceType.ID != 2));
            return View("Index", serviceActions.ToList());
        }
        [Authorize(Roles = "BagService,Management")]
        public ActionResult BagServiceList(int? id)
        {
            var serviceActions = db.ServiceActions.Include(s => s.AspNetUser)
                .Include(s => s.Clinic).Include(s => s.Family)
                .Include(s => s.Hospital).Include(s => s.Person)
                .Include(s => s.Servant).Include(s => s.ServiceType)
                .Where(x => x.IsActive == true && x.ServiceType.ID == 1);

            if (id != null)
                serviceActions = serviceActions.Where(x => x.PersonID == id);

            return View("Index", serviceActions.ToList());
        }

        [Authorize(Roles = "Management,Health")]
        public ActionResult MedicalServiceList(int? id)
        {
            var serviceActions = db.ServiceActions.Include(s => s.AspNetUser)
                                    .Include(s => s.Clinic).Include(s => s.Family)
                                    .Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant)
                                    .Include(s => s.ServiceType).Where(x => x.IsActive == true && x.ServiceType.ID == 2);
            if (id != null)
                serviceActions = serviceActions.Where(x => x.PersonID == id);

            return View("MedicalServiceList", serviceActions.ToList());
        }

        [Authorize(Roles = "Management,Health")]
        public ActionResult getClinicServices(int? id)
        {
            var serviceActions = db.ServiceActions.Include(s => s.AspNetUser)
                                    .Include(s => s.Clinic).Include(s => s.Family)
                                    .Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant)
                                    .Include(s => s.ServiceType).Where(x => x.IsActive == true && x.ServiceType.ID == 2);
            if (id != null)
                serviceActions = serviceActions.Where(x => x.ClinicID == id);

            return View("MedicalServiceList", serviceActions.ToList());
        }

        [Authorize(Roles = "Management,Health")]
        public ActionResult getHospitalServices(int? id)
        {
            var serviceActions = db.ServiceActions.Include(s => s.AspNetUser)
                                    .Include(s => s.Clinic).Include(s => s.Family)
                                    .Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant)
                                    .Include(s => s.ServiceType).Where(x => x.IsActive == true && x.ServiceType.ID == 2);
            if (id != null)
                serviceActions = serviceActions.Where(x => x.HospitalID == id);

            return View("MedicalServiceList", serviceActions.ToList());
        }

        [Authorize(Roles = "Management")]
        public ActionResult FinancialServiceList(int? id)
        {
            var serviceActions = db.ServiceActions.Include(s => s.AspNetUser)
                                    .Include(s => s.Clinic).Include(s => s.Family)
                                    .Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant)
                                    .Include(s => s.ServiceType).Where(x => x.IsActive == true && x.ServiceType.ID == 3);
            if (id != null)
                serviceActions = serviceActions.Where(x => x.PersonID == id);

            return View("Index", serviceActions.ToList());
        }

        [Authorize(Roles = "Management")]
        public ActionResult AllServicesList(int? id)
        {
            var serviceActions = db.ServiceActions.Include(s => s.AspNetUser)
                        .Include(s => s.Clinic).Include(s => s.Family)
                        .Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant)
                        .Include(s => s.ServiceType).Where(x => x.IsActive == true);
            if (id != null)
                serviceActions = serviceActions.Where(x => x.PersonID == id);

            return View("Index", serviceActions.ToList());
        }

        [Authorize(Roles = "Management")]
        public ActionResult GetServicesByFamily(int? FamilyID)
        {
            var serviceActions = db.ServiceActions.Include(s => s.AspNetUser)
                        .Include(s => s.Clinic).Include(s => s.Family)
                        .Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant)
                        .Include(s => s.ServiceType).Where(x => x.IsActive == true);
            if (FamilyID != null)
                serviceActions = serviceActions.Where(x => x.Person.FamilyID == FamilyID);

            return View("Index", serviceActions.ToList());
        }

        public ActionResult Create()
        {
            ResetData();
            return View();
        }

        private void ResetData()
        {
            ViewBag.UserID = new SelectList(db.AspNetUsers.Where(x => x.LockoutEnabled == true), "Id", "Email");
            ViewBag.ClinicID = new SelectList(db.Clinics.Where(x => x.IsActive == true), "ID", "ClinicName");
            ViewBag.FamilyID = new SelectList(db.Families.Where(x => x.IsActive == true), "ID", "FamilyCode");
            ViewBag.HospitalID = new SelectList(db.Hospitals.Where(x => x.IsActive == true), "ID", "HospitalName");
            ViewBag.PersonID = new SelectList(db.People.Where(x => x.IsActive == true), "ID", "FullName");
            ViewBag.ServantID = new SelectList(db.Servants.Where(x => x.IsActive == true), "ID", "ServantName");
            ViewBag.ActionTypeID = new SelectList(db.ServiceTypes.Where(x => x.IsActive == true), "ID", "ActionType");
            ViewBag.MedicalContractID = new SelectList(db.MedicalContracts.Where(x => x.IsActive == true), "ID", "Notes");
            ViewBag.MedicalServiceTypeID = new SelectList(db.MedicalServices.Where(x => x.IsActive == true), "ID", "MedicalService1");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                serviceAction.UserID = User.Identity.GetUserId();
                serviceAction.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                serviceAction.PersonID = Convert.ToInt32(Request.Form["PersonID"].ToString());
                serviceAction.ActionTypeID = Convert.ToInt32(Request.Form["ActionTypeID"].ToString());
                serviceAction.IsActive = true;
                serviceAction.EntryDate = DateTime.Now;
                var p = db.People.Where(x => x.ID == serviceAction.PersonID).FirstOrDefault();
                serviceAction.FamilyID = p.FamilyID;
                db.ServiceActions.Add(serviceAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ResetData();
            return View(serviceAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Health")]
        public ActionResult AddHealthyService(ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                serviceAction.UserID = User.Identity.GetUserId();
                serviceAction.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                serviceAction.PersonID = Convert.ToInt32(Request.Form["PersonID"].ToString());
                serviceAction.ActionTypeID = 2;
                serviceAction.IsActive = true;
                serviceAction.EntryDate = DateTime.Now;
                var p = db.People.Where(x => x.ID == serviceAction.PersonID).FirstOrDefault();
                serviceAction.FamilyID = p.FamilyID;
                db.ServiceActions.Add(serviceAction);
                db.SaveChanges();
                return RedirectToAction("MedicalServiceList");
            }

            ResetData();
            return View(serviceAction);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Health")]
        public ActionResult AddHealthyClinicService(ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                serviceAction.UserID = User.Identity.GetUserId();
                serviceAction.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                serviceAction.PersonID = Convert.ToInt32(Request.Form["PersonID"].ToString());
                serviceAction.ActionTypeID = 2;
                serviceAction.IsActive = true;
                serviceAction.EntryDate = DateTime.Now;
                var p = db.People.Where(x => x.ID == serviceAction.PersonID).FirstOrDefault();
                serviceAction.FamilyID = p.FamilyID;
                db.ServiceActions.Add(serviceAction);
                db.SaveChanges();
                return RedirectToAction("MedicalServiceList");
            }

            ResetData();
            return View(serviceAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,BagService")]
        public ActionResult AddBagService(ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                serviceAction.UserID = User.Identity.GetUserId();
                serviceAction.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                serviceAction.PersonID = Convert.ToInt32(Request.Form["PersonID"].ToString());
                serviceAction.ActionTypeID = 1;
                serviceAction.IsActive = true;
                serviceAction.EntryDate = DateTime.Now;
                var p = db.People.Where(x => x.ID == serviceAction.PersonID).FirstOrDefault();
                serviceAction.FamilyID = p.FamilyID;
                db.ServiceActions.Add(serviceAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ResetData();
            return View(serviceAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management")]
        public ActionResult AddFinancialService(ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                serviceAction.UserID = User.Identity.GetUserId();
                serviceAction.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                serviceAction.PersonID = Convert.ToInt32(Request.Form["PersonID"].ToString());
                serviceAction.ActionTypeID = 3;
                serviceAction.IsActive = true;
                serviceAction.EntryDate = DateTime.Now;
                var p = db.People.Where(x => x.ID == serviceAction.PersonID).FirstOrDefault();
                serviceAction.FamilyID = p.FamilyID;
                db.ServiceActions.Add(serviceAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ResetData();
            return View(serviceAction);
        }

        [Authorize(Roles = "Management,Health")]
        public ActionResult AddHealthyService()
        {
            ResetData();
            return View();
        }

        [Authorize(Roles = "Management,Health")]
        public ActionResult AddHealthyClinicService()
        {
            ResetData();
            return View();
        }
        [Authorize(Roles = "Management")]
        public ActionResult AddFinancialService()
        {
            ResetData();
            return View();
        }
        [Authorize(Roles = "Management,BagService")]
        public ActionResult AddBagService()
        {
            ResetData();
            return View();
        }

        [Authorize(Roles = "Management")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceAction serviceAction = db.ServiceActions.Find(id);
            if (serviceAction == null)
            {
                return HttpNotFound();
            }

            ResetData();
            return View(serviceAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management")]
        public ActionResult Edit(ServiceAction serviceAction)
        {
            if (ModelState.IsValid)
            {
                //serviceAction.UserID = User.Identity.GetUserId();
                serviceAction.ServantID = Convert.ToInt32(Request.Form["ServantID"].ToString());
                serviceAction.PersonID = Convert.ToInt32(Request.Form["PersonID"].ToString());
                serviceAction.IsActive = true;
                serviceAction.UpdatedOn = DateTime.Now;
                serviceAction.UpdateBy = User.Identity.GetUserId();
                db.Entry(serviceAction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ResetData();
            return View(serviceAction);
        }

        [HttpPost]
        [Authorize(Roles = "Management")]
        public ActionResult DeleteAction(int id)
        {
            ServiceAction serviceAction = db.ServiceActions.Find(id);
            serviceAction.IsActive = false;
            db.SaveChanges();
            return Json(data: new { success = true, message = "Service has been deleted successfully" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Management,Health")]
        public ActionResult GetMedicalContractsByClinic(string clinicID)
        {
            List<SelectListItem> MedicalContracts = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(clinicID))
            {
                int cID = Convert.ToInt32(clinicID);
                List<MedicalContract> contracts = db.MedicalContracts.Where(x => x.ClinicID == cID).ToList();
                contracts.ForEach(x =>
                {
                    MedicalContracts.Add(new SelectListItem { Text = x.Notes, Value = x.ID.ToString() });
                });
            }
            return Json(MedicalContracts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Management,Health")]
        public ActionResult GetMedicalContractsByHospital(string hospitalID)
        {
         
            List<SelectListItem> MedicalContracts = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(hospitalID))
            {
                int cID = Convert.ToInt32(hospitalID);
                List<MedicalContract> contracts = db.MedicalContracts.Where(x => x.HospitalID == cID).ToList();
                contracts.ForEach(x =>
                {
                    MedicalContracts.Add(new SelectListItem { Text = x.Notes, Value = x.ID.ToString() });
                });
            }
            return Json(MedicalContracts, JsonRequestBehavior.AllowGet);
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
