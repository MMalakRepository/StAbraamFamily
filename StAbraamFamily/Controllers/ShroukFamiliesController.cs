using StAbraamFamily.Web.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StAbraamFamily.Controllers
{
    [Authorize(Roles = "Management")]
    public class ShroukFamiliesController : Controller
    {
        private readonly IUnitOfWork saintUnits;
        public ShroukFamiliesController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult DataReport()
        {
            var familiesData = saintUnits.ShroukFamilies.GetAll();
            return View(familiesData.ToList());
        }
    }
}