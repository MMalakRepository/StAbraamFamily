using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StAbraamFamily.Controllers
{
    public class HealthReservationsController : Controller
    {
        private readonly IUnitOfWork saintUnits;

        public HealthReservationsController(IUnitOfWork SaintUnits)
        {
            saintUnits = SaintUnits;
        }

        [Authorize(Roles = "HealthManagment,Management")]
        public ActionResult ListOfReservations()
        {
            return View(saintUnits.HealthReservations.GetAll().Where(x => x.IsDeleted == false).ToList());
        }

        [AllowAnonymous]
        public ActionResult AddNewReservation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewReservation(CovidReservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.FullName = reservation.FirstName + " " + reservation.SecondName + " " + reservation.ThirdName + " " + reservation.FourthName;
                reservation.CreatedOn = DateTime.Now;
                reservation.IsDeleted = false;
                reservation.IsFinished = false;
                saintUnits.HealthReservations.Add(reservation);
                saintUnits.Complete();
                return View(new CovidReservation());
            }

            return View(reservation);
        }

        [Authorize(Roles = "HealthManagment,Management")]
        public ActionResult EditReservation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CovidReservation reservation = saintUnits.HealthReservations.Get(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReservation(CovidReservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.FullName = reservation.FirstName + " " + reservation.SecondName + " " + reservation.ThirdName + " " + reservation.FourthName;
                reservation.IsDeleted = false;
                reservation.IsFinished = false;
                reservation.UpdatedBy = User.Identity.Name;
                reservation.UpdatedOn = DateTime.Now;
                saintUnits.HealthReservations.Update(reservation);
                saintUnits.Complete();
                return RedirectToAction("ListOfReservations");
            }
            return View(reservation);
        }

        [HttpPost]
        [Authorize(Roles = "HealthManagment,Management")]
        public ActionResult DeleteReservation(int ID)
        {
            CovidReservation reservation = saintUnits.HealthReservations.Get(ID);
            saintUnits.HealthReservations.Remove(reservation);
            saintUnits.Complete();
            return Json(data: new { success = true, message = "Reservation has been deleted successfully" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsReservationNumberAvailable(string ReservationNo)
        {
            List<CovidReservation> reservations = saintUnits.HealthReservations.Find(x => x.ReservationNumber == ReservationNo).ToList();
            if(reservations == null)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNationalIDAvailable(string NationalID)
        {
            List<CovidReservation> reservations = saintUnits.HealthReservations.Find(x => x.NationalID == NationalID).ToList();
            if (reservations == null)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
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