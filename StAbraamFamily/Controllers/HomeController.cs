using StAbraamFamily.Models;
using StAbraamFamily.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StAbraamFamily.Controllers
{
    public class HomeController : Controller
    {
        private StAbraamEntities db = new StAbraamEntities();
 

        public ActionResult Home()
        {
            SystemDashboard systemDashboard = new SystemDashboard()
            {
                Fathers = db.Fathers.Where(x => x.IsActive == true).ToList().Count(),
                Servants = db.Servants.Where(x => x.IsActive == true).ToList().Count(),
                Families = db.Families.Where(x => x.IsActive == true).ToList().Count(),
                FamilyServants = db.FamilyServants.Where(x => x.IsActive == true).ToList().Count(),
                BagServices = db.ServiceActions.Where(x => x.IsActive == true && x.ServiceType.ID == 1).ToList().Count(),
                MedicalServices = db.ServiceActions.Where(x => x.IsActive == true && x.ServiceType.ID == 2).ToList().Count(),
                ServiceActions = db.ServiceActions.Where(x => x.IsActive == true).ToList().Count(),
                Children = db.Children.Where(x => x.IsActive == true).ToList().Count(),
                People = db.People.Where(x => x.IsActive == true).ToList().Count(),
                FamilyVisits = db.FamilyVisits.ToList().Count()
            };

            return View(systemDashboard);
        }

    }



}