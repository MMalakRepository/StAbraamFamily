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
using StAbraamFamily.Web.Entities.Domain;
using StAbraamFamily.Web.Core.Repositories;

namespace StAbraamFamily.Controllers
{

    public class ServiceActionsController : Controller
    {
        private SaintAbraamEntities db = new SaintAbraamEntities();
        private readonly IUnitOfWork saintUnits;

        public ServiceActionsController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        [Authorize(Roles = "Management")]
        public ActionResult Index()
        {
            var serviceActions = saintUnits.ServiceActions.GetAllServiceDetails();
            return View("Index", serviceActions.ToList());
        }
        [Authorize(Roles = "BagService,Management")]
        public ActionResult BagServiceList(int? id)
        {
            var serviceActions = saintUnits.ServiceActions.GetBagServices();

            if (id != null)
                serviceActions = serviceActions.Where(x => x.PersonID == id);

            return View("Index", serviceActions.ToList());
        }

        [Authorize(Roles = "Management,Health")]
        public ActionResult MedicalServiceList(int? id)
        {
            var serviceActions = saintUnits.ServiceActions.GetAllMEdicalService();
            if (id != null)
                serviceActions = serviceActions.Where(x => x.PersonID == id);

            return View("MedicalServiceList", serviceActions.ToList());
        }

        [Authorize(Roles = "Management,Health")]
        public ActionResult getClinicServices(int? id)
        {
            var serviceActions = saintUnits.ServiceActions.GetAllMEdicalService();
            if (id != null)
                serviceActions = serviceActions.Where(x => x.ClinicID == id);

            return View("MedicalServiceList", serviceActions.ToList());
        }

        [Authorize(Roles = "Management,Health")]
        public ActionResult getHospitalServices(int? id)
        {
            var serviceActions = saintUnits.ServiceActions.GetAllMEdicalService();
            if (id != null)
                serviceActions = serviceActions.Where(x => x.HospitalID == id);

            return View("MedicalServiceList", serviceActions.ToList());
        }

        [Authorize(Roles = "Management")]
        public ActionResult FinancialServiceList(int? id)
        {
            var serviceActions = saintUnits.ServiceActions.GetFinancialServices();
            if (id != null)
                serviceActions = serviceActions.Where(x => x.PersonID == id);

            return View("Index", serviceActions.ToList());
        }

        [Authorize(Roles = "Management")]
        public ActionResult AllServicesList(int? id)
        {
            var serviceActions = saintUnits.ServiceActions.GetAllServiceDetails();
            if (id != null)
                serviceActions = serviceActions.Where(x => x.PersonID == id);

            return View("Index", serviceActions.ToList());
        }

        [Authorize(Roles = "Management")]
        public ActionResult GetServicesByFamily(int? FamilyID)
        {
            var serviceActions = saintUnits.ServiceActions.GetAllServiceDetails();
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
            ViewBag.ClinicID = new SelectList(saintUnits.Clinics.Find(x => x.IsActive == true), "ID", "ClinicName");
            ViewBag.FamilyID = new SelectList(saintUnits.Families.Find(x => x.IsActive == true), "ID", "FamilyCode");
            ViewBag.HospitalID = new SelectList(saintUnits.Hospitals.Find(x => x.IsActive == true), "ID", "HospitalName");
            ViewBag.PersonID = new SelectList(saintUnits.People.Find(x => x.IsActive == true), "ID", "FullName");
            ViewBag.ServantID = new SelectList(saintUnits.Servants.Find(x => x.IsActive == true), "ID", "ServantName");
            ViewBag.ActionTypeID = new SelectList(saintUnits.ServiceTypes.Find(x => x.IsActive == true), "ID", "ActionType");
            ViewBag.MedicalContractID = new SelectList(db.MedicalContracts.Where(x => x.IsActive == true), "ID", "Notes");
            ViewBag.MedicalServiceTypeID = new SelectList(saintUnits.MedicalServices.Find(x => x.IsActive == true), "ID", "MedicalService1");
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
                var p = saintUnits.People.SingleOrDefault(x => x.ID == serviceAction.PersonID);
                serviceAction.FamilyID = p.FamilyID;
                saintUnits.ServiceActions.Add(serviceAction);
                saintUnits.Complete();
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
                var p = saintUnits.People.SingleOrDefault(x => x.ID == serviceAction.PersonID);
                serviceAction.FamilyID = p.FamilyID;
                saintUnits.ServiceActions.Add(serviceAction);
                saintUnits.Complete();
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
                var p = saintUnits.People.SingleOrDefault(x => x.ID == serviceAction.PersonID);
                serviceAction.FamilyID = p.FamilyID;
                saintUnits.ServiceActions.Add(serviceAction);
                saintUnits.Complete();
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
                var p = saintUnits.People.SingleOrDefault(x => x.ID == serviceAction.PersonID);
                serviceAction.FamilyID = p.FamilyID;
                saintUnits.ServiceActions.Add(serviceAction);
                saintUnits.Complete();
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
                var p = saintUnits.People.SingleOrDefault(x => x.ID == serviceAction.PersonID);
                serviceAction.FamilyID = p.FamilyID;
                saintUnits.ServiceActions.Add(serviceAction);
                saintUnits.Complete();
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
            ServiceAction serviceAction = saintUnits.ServiceActions.Get(id);
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
                saintUnits.ServiceActions.Update(serviceAction);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            ResetData();
            return View(serviceAction);
        }

        [HttpPost]
        [Authorize(Roles = "Management")]
        public ActionResult DeleteAction(int id)
        {
            ServiceAction serviceAction = saintUnits.ServiceActions.Get(id);
            saintUnits.ServiceActions.Remove(serviceAction);
            saintUnits.Complete();
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
                List<MedicalContract> contracts = saintUnits.MedicalContracts.Find(x => x.ClinicID == cID).ToList();
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
                List<MedicalContract> contracts = saintUnits.MedicalContracts.Find(x => x.HospitalID == cID).ToList();
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
                saintUnits.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
