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
    public class MedicalServicesController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();

        // GET: MedicalServices
        public async Task<ActionResult> Index()
        {
            return View(await db.MedicalServices.ToListAsync());
        }

        // GET: MedicalServices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalService medicalService = await db.MedicalServices.FindAsync(id);
            if (medicalService == null)
            {
                return HttpNotFound();
            }
            return View(medicalService);
        }

        // GET: MedicalServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,MedicalService1,Notes,IsActive")] MedicalService medicalService)
        {
            if (ModelState.IsValid)
            {
                db.MedicalServices.Add(medicalService);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(medicalService);
        }

        // GET: MedicalServices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalService medicalService = await db.MedicalServices.FindAsync(id);
            if (medicalService == null)
            {
                return HttpNotFound();
            }
            return View(medicalService);
        }

        // POST: MedicalServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MedicalService1,Notes,IsActive")] MedicalService medicalService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalService).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(medicalService);
        }

        // GET: MedicalServices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalService medicalService = await db.MedicalServices.FindAsync(id);
            if (medicalService == null)
            {
                return HttpNotFound();
            }
            return View(medicalService);
        }

        // POST: MedicalServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalService medicalService = await db.MedicalServices.FindAsync(id);
            db.MedicalServices.Remove(medicalService);
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
