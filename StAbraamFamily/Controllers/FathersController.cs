using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Controllers
{
    [Authorize(Roles = "Management")]
    public class FathersController : Controller
    {
        private readonly IUnitOfWork saintUnits;
        public FathersController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            return View(saintUnits.Fathers.Find(x => x.IsActive ==true).ToList());
        }
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Father father)
        {
            if (ModelState.IsValid)
            {
                father.IsActive = true;
                saintUnits.Fathers.Add(father);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            return View(father);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Father father = saintUnits.Fathers.Get(id);
            if (father == null)
            {
                return HttpNotFound();
            }
            return View(father);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Father father)
        {
            if (ModelState.IsValid)
            {
                father.IsActive = true;
                saintUnits.Fathers.Update(father);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }
            return View(father);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            Father father = saintUnits.Fathers.Get(id);
            saintUnits.Fathers.Remove(father);
            saintUnits.Complete();
            return Json(data : new { success = true , message ="Father has been deleted successfully"},JsonRequestBehavior.AllowGet);
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
