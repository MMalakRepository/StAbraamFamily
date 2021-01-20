using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StAbraamFamily.Models;

namespace StAbraamFamily.Controllers
{
    public class MedicalContractsController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();
        public async Task<ActionResult> Index()
        {
            var medicalContracts = db.MedicalContracts.Where(x => x.IsFinished == false && x.IsActive == true).Include(m => m.Clinic).Include(m => m.Hospital).Include(m => m.MedicalService);
            return View(await medicalContracts.ToListAsync());
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalContract medicalContract = await db.MedicalContracts.FindAsync(id);
            if (medicalContract == null)
            {
                return HttpNotFound();
            }
            return View(medicalContract);
        }
        public ActionResult Create()
        {
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName");
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName");
            ViewBag.MedicalServiceID = new SelectList(db.MedicalServices, "ID", "MedicalService1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MedicalContract medicalContract)
        {
            if (ModelState.IsValid)
            {
                medicalContract.IsFinished = false;
                medicalContract.IsActive = true;
                medicalContract.EntryDate = DateTime.Now;
                medicalContract.EnteredBy = User.Identity.Name;
                db.MedicalContracts.Add(medicalContract);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ResetData(medicalContract);
            return View(medicalContract);
        }

        private void ResetData(MedicalContract medicalContract)
        {
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName", medicalContract.ClinicID);
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName", medicalContract.HospitalID);
            ViewBag.MedicalServiceID = new SelectList(db.MedicalServices, "ID", "MedicalService1", medicalContract.MedicalServiceID);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalContract medicalContract = await db.MedicalContracts.FindAsync(id);
            if (medicalContract == null)
            {
                return HttpNotFound();
            }
            ResetData(medicalContract);

            return View(medicalContract);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MedicalContract medicalContract)
        {
            if (ModelState.IsValid)
            {
                medicalContract.IsFinished = false;
                medicalContract.IsActive = true;
                db.Entry(medicalContract).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ResetData(medicalContract);

            return View(medicalContract);
        }
 
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalContract medicalContract = await db.MedicalContracts.FindAsync(id);
            if (medicalContract == null)
            {
                return HttpNotFound();
            }
            return View(medicalContract);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            MedicalContract medicalContract = db.MedicalContracts.Find(id);
            medicalContract.IsActive = false;
            db.SaveChanges();
            return Json(data: new { success = true, message = "Medical Contract deleted successfully" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FinishAction(int id)
        {
            MedicalContract medicalContract = db.MedicalContracts.Find(id);
            medicalContract.IsActive = true;
            medicalContract.IsFinished = true;
            db.SaveChanges();
            return Json(data: new { success = true, message = "Medical Contract Finished successfully" }, JsonRequestBehavior.AllowGet);
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
