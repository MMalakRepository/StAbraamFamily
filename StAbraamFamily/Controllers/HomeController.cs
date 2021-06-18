using System.Linq;
using System.Web.Mvc;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entitie.ViewModels;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork saintUnits;

        public HomeController(IUnitOfWork saintUnits)
        {
            this.saintUnits = saintUnits;
        }
        public ActionResult Home()
        {
            SystemDashboard systemDashboard = new SystemDashboard()
            {
                Fathers = saintUnits.Fathers.Find(x => x.IsActive == true).Count(),
                Servants = saintUnits.Servants.Find(x => x.IsActive == true).Count(),
                Families = saintUnits.Families.Find(x => x.IsActive == true).Count(),
                FamilyServants = saintUnits.FamilyServants.Find(x => x.IsActive == true).Count(),
                BagServices = saintUnits.ServiceActions.Find(x => x.IsActive == true && x.ServiceType.ID == 1).Count(),
                MedicalServices = saintUnits.ServiceActions.Find(x => x.IsActive == true && x.ServiceType.ID == 2).Count(),
                ServiceActions = saintUnits.ServiceActions.Find(x => x.IsActive == true).Count(),  
                Children = saintUnits.Children.Find(x => x.IsActive == true).Count(),
                People = saintUnits.People.Find(x => x.IsActive == true).Count(),
                FamilyVisits = saintUnits.FamilyVisits.GetAll().Count(),
                serviceTypes = saintUnits.ServiceTypes.Find(x => x.IsActive == true).Count(),
                Clinics = saintUnits.Clinics.Find(x => x.IsActive == true).Count(),
                Hospitals = saintUnits.Hospitals.Find(x => x.IsActive == true).Count(),
                FinancialServices = saintUnits.ServiceActions.Find(x => x.IsActive == true && x.ServiceType.ID == 3).Count()
            };

            return View(systemDashboard);
        }

    }



}