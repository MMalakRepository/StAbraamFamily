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
    [Authorize(Roles ="Management,Health")]
    public class MedicalServicesController : Controller
    {
        private readonly IUnitOfWork saintUints;

        public MedicalServicesController(IUnitOfWork saintUints)
        {
            this.saintUints = saintUints;
        }
        public ActionResult Index()
        {
            return View(saintUints.MedicalServices.Find(x => x.IsActive == true));
        }
 
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalService medicalService =  saintUints.MedicalServices.Get(id);
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
        public ActionResult Create(MedicalService medicalService)
        {
            if (ModelState.IsValid)
            {
                medicalService.IsActive = true;
                saintUints.MedicalServices.Add(medicalService);
                saintUints.Complete();
                return RedirectToAction("Index");
            }

            return View(medicalService);
        }
 
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalService medicalService =  saintUints.MedicalServices.Get(id);
            if (medicalService == null)
            {
                return HttpNotFound();
            }
            return View(medicalService);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicalService medicalService)
        {
            if (ModelState.IsValid)
            {
                medicalService.IsActive = true;
                saintUints.MedicalServices.Update(medicalService);
                saintUints.Complete();
                return RedirectToAction("Index");
            }
            return View(medicalService);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalService medicalService = saintUints.MedicalServices.Get(id);
            if (medicalService == null)
            {
                return HttpNotFound();
            }
            return View(medicalService);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            MedicalService medicalService = saintUints.MedicalServices.Get(id);
            saintUints.MedicalServices.Remove(medicalService);
            saintUints.Complete();
            return Json(data: new { success = true, message = "Medical Service deleted successfully" }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                saintUints.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
