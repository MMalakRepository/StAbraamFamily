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
                Fathers = db.Fathers.Where(x => x.IsActive == true).Count(),
                Servants = db.Servants.Where(x => x.IsActive == true).Count(),
                Families = db.Families.Where(x => x.IsActive == true).Count(),
                FamilyServants = db.FamilyServants.Where(x => x.IsActive == true).Count(),
                BagServices = db.ServiceActions.Where(x => x.IsActive == true && x.ServiceType.ID == 1).Count(),
                MedicalServices = db.ServiceActions.Where(x => x.IsActive == true && x.ServiceType.ID == 2).Count(),
                ServiceActions = db.ServiceActions.Where(x => x.IsActive == true).Count(),  
                Children = db.Children.Where(x => x.IsActive == true).Count(),
                People = db.People.Where(x => x.IsActive == true).Count(),
                FamilyVisits = db.FamilyVisits.Count(),
                serviceTypes = db.ServiceTypes.Where(x => x.IsActive == true).Count(),
                Clinics = db.Clinics.Where(x => x.IsActive == true).Count(),
                Hospitals = db.Hospitals.Where(x => x.IsActive == true).Count(),
                FinancialServices = db.ServiceActions.Where(x => x.IsActive == true && x.ServiceType.ID == 3).Count()
            };

            return View(systemDashboard);
        }

    }



}