using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using StAbraamFamily.Web.Entities.Domain;
using StAbraamFamily.Web.Core.Repositories;

namespace StAbraamFamily.Controllers
{
    [Authorize(Roles = "Management,Health")]
    public class MedicalContractsController : Controller
    {
        private readonly IUnitOfWork saintUnits;

        public MedicalContractsController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            var medicalContracts = saintUnits.MedicalContracts.GetMedicalCOntractDetails();
            return View(medicalContracts);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalContract medicalContract = saintUnits.MedicalContracts.Get(id);
            if (medicalContract == null)
            {
                return HttpNotFound();
            }
            return View(medicalContract);
        }
        public ActionResult Create()
        {
            ViewBag.ClinicID = new SelectList(saintUnits.Clinics.GetAll(), "ID", "ClinicName");
            ViewBag.HospitalID = new SelectList(saintUnits.Hospitals.GetAll(), "ID", "HospitalName");
            ViewBag.MedicalServiceID = new SelectList(saintUnits.MedicalServices.GetAll(), "ID", "MedicalService1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalContract medicalContract)
        {
            if (ModelState.IsValid)
            {
                medicalContract.IsFinished = false;
                medicalContract.IsActive = true;
                medicalContract.EntryDate = DateTime.Now;
                medicalContract.EnteredBy = User.Identity.Name;
                saintUnits.MedicalContracts.Add(medicalContract);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }
            ResetData(medicalContract);
            return View(medicalContract);
        }

        private void ResetData(MedicalContract medicalContract)
        {
            ViewBag.ClinicID = new SelectList(saintUnits.Clinics.GetAll(), "ID", "ClinicName", medicalContract.ClinicID);
            ViewBag.HospitalID = new SelectList(saintUnits.Hospitals.GetAll(), "ID", "HospitalName", medicalContract.HospitalID);
            ViewBag.MedicalServiceID = new SelectList(saintUnits.MedicalServices.GetAll(), "ID", "MedicalService1", medicalContract.MedicalServiceID);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalContract medicalContract = saintUnits.MedicalContracts.Get(id);
            if (medicalContract == null)
            {
                return HttpNotFound();
            }
            ResetData(medicalContract);

            return View(medicalContract);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicalContract medicalContract)
        {
            if (ModelState.IsValid)
            {
                medicalContract.IsFinished = false;
                medicalContract.IsActive = true;
                saintUnits.MedicalContracts.Update(medicalContract);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }
            ResetData(medicalContract);

            return View(medicalContract);
        }
 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalContract medicalContract = saintUnits.MedicalContracts.Get(id);
            if (medicalContract == null)
            {
                return HttpNotFound();
            }
            return View(medicalContract);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            MedicalContract medicalContract = saintUnits.MedicalContracts.Get(id);
            saintUnits.MedicalContracts.Remove(medicalContract);
            saintUnits.Complete();
            return Json(data: new { success = true, message = "Medical Contract deleted successfully" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FinishAction(int id)
        {
            MedicalContract medicalContract = saintUnits.MedicalContracts.Get(id);
            medicalContract.IsActive = true;
            medicalContract.IsFinished = true;
            saintUnits.MedicalContracts.Update(medicalContract);
            saintUnits.Complete();
            return Json(data: new { success = true, message = "Medical Contract Finished successfully" }, JsonRequestBehavior.AllowGet);
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
