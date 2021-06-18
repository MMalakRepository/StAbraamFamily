using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Controllers
{
    [Authorize(Roles ="Management")]
    public class EvaluationLevelsController : Controller
    { 
    
        private readonly IUnitOfWork saintUnits;
        public EvaluationLevelsController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Index()
        {
            return View(saintUnits.EvaluationLevels.GetAll().Where(x => x.IsActive == true));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvaluationLevel evaluationLevel)
        {
            if (ModelState.IsValid)
            {
                saintUnits.EvaluationLevels.Add(evaluationLevel);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }

            return View(evaluationLevel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationLevel evaluationLevel = saintUnits.EvaluationLevels.Get(id);
            if (evaluationLevel == null)
            {
                return HttpNotFound();
            }
            return View(evaluationLevel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EvaluationLevel evaluationLevel)
        {
            if (ModelState.IsValid)
            {
                saintUnits.EvaluationLevels.Update(evaluationLevel);
                saintUnits.Complete();
                return RedirectToAction("Index");
            }
            return View(evaluationLevel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationLevel evaluationLevel = saintUnits.EvaluationLevels.Get(id);
            if (evaluationLevel == null)
            {
                return HttpNotFound();
            }
            return View(evaluationLevel);
        }

        [HttpPost]
        public ActionResult DeleteAction(int id)
        {
            EvaluationLevel evaluationLevel = saintUnits.EvaluationLevels.Get(id);
            saintUnits.EvaluationLevels.Remove(evaluationLevel);
            saintUnits.Complete();
            return RedirectToAction("Index");
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
