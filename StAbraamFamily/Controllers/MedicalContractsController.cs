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

        // GET: MedicalContracts
        public async Task<ActionResult> Index()
        {
            var medicalContracts = db.MedicalContracts.Include(m => m.Clinic).Include(m => m.Hospital).Include(m => m.MedicalService);
            return View(await medicalContracts.ToListAsync());
        }

        // GET: MedicalContracts/Details/5
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

        // GET: MedicalContracts/Create
        public ActionResult Create()
        {
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName");
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName");
            ViewBag.MedicalServiceID = new SelectList(db.MedicalServices, "ID", "MedicalService1");
            return View();
        }

        // POST: MedicalContracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,MedicalServiceID,HospitalID,ClinicID,Notes,IsFinished,Percentage,EntryDate,EnteredBy")] MedicalContract medicalContract)
        {
            if (ModelState.IsValid)
            {
                db.MedicalContracts.Add(medicalContract);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName", medicalContract.ClinicID);
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName", medicalContract.HospitalID);
            ViewBag.MedicalServiceID = new SelectList(db.MedicalServices, "ID", "MedicalService1", medicalContract.MedicalServiceID);
            return View(medicalContract);
        }

        // GET: MedicalContracts/Edit/5
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
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName", medicalContract.ClinicID);
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName", medicalContract.HospitalID);
            ViewBag.MedicalServiceID = new SelectList(db.MedicalServices, "ID", "MedicalService1", medicalContract.MedicalServiceID);
            return View(medicalContract);
        }

        // POST: MedicalContracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MedicalServiceID,HospitalID,ClinicID,Notes,IsFinished,Percentage,EntryDate,EnteredBy")] MedicalContract medicalContract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalContract).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClinicID = new SelectList(db.Clinics, "ID", "ClinicName", medicalContract.ClinicID);
            ViewBag.HospitalID = new SelectList(db.Hospitals, "ID", "HospitalName", medicalContract.HospitalID);
            ViewBag.MedicalServiceID = new SelectList(db.MedicalServices, "ID", "MedicalService1", medicalContract.MedicalServiceID);
            return View(medicalContract);
        }

        // GET: MedicalContracts/Delete/5
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

        // POST: MedicalContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalContract medicalContract = await db.MedicalContracts.FindAsync(id);
            db.MedicalContracts.Remove(medicalContract);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
