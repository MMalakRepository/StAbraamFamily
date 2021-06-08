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
    [Authorize(Roles ="Management,Health")]
    public class MedicalServicesController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();
 
        public async Task<ActionResult> Index()
        {
            return View(await db.MedicalServices.Where(x => x.IsActive == true).ToListAsync());
        }
 
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

 
        public ActionResult Create()
        {
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,MedicalService1,Notes,IsActive")] MedicalService medicalService)
        {
            if (ModelState.IsValid)
            {
                medicalService.IsActive = true;
                db.MedicalServices.Add(medicalService);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(medicalService);
        }
 
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
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MedicalService1,Notes,IsActive")] MedicalService medicalService)
        {
            if (ModelState.IsValid)
            {
                medicalService.IsActive = true;
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


        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            MedicalService medicalService = db.MedicalServices.Find(id);
            medicalService.IsActive = false;
            db.SaveChanges();
            return Json(data: new { success = true, message = "Medical Service deleted successfully" }, JsonRequestBehavior.AllowGet);
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
