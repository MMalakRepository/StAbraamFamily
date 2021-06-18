using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Controllers
{
    [Authorize(Roles = "Management,Health")]
    public class HospitalsController : Controller
    {

        private readonly IUnitOfWork saintUnits;
        public HospitalsController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            return View(saintUnits.Hospitals.Find(x => x.IsActive == true).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                hospital.IsActive = true;
                saintUnits.Hospitals.Add(hospital);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            return View(hospital);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital hospital = saintUnits.Hospitals.Get(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }
            return View(hospital);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                hospital.IsActive = true;
                saintUnits.Hospitals.Update(hospital);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }
            return View(hospital);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            Hospital hospital = saintUnits.Hospitals.Get(id);
            saintUnits.Hospitals.Remove(hospital);
            saintUnits.Complete();
            return Json(data: new { success = true, message = "Hospital deleted successfully" }, JsonRequestBehavior.AllowGet);
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
