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
            string fullName = reservation.FirstName + " " + reservation.SecondName + " " + reservation.ThirdName + " " + reservation.FourthName;
            var t = saintUnits.HealthReservations.Find(x => x.FullName == fullName && x.IsDeleted == false).ToList();
            if (t != null && t.Count > 0)
            {
                ModelState.AddModelError("FullName", "تم أدخال هذا الأسم من قبل");
                ViewBag.Message = "تم أدخال هذا الأسم من قبل";
            }
            t = saintUnits.HealthReservations.Find(x => x.MobileNumber == reservation.MobileNumber && x.IsDeleted == false).ToList();
            if (t != null && t.Count > 0)
            {
                ModelState.AddModelError("MobileNumber", "تم أدخال رقم الهاتف من قبل");
                ViewBag.Message = "تم أدخال رقم الهاتف من قبل";
            }

            if (ModelState.IsValid)
            {
                reservation.FullName = fullName;
                reservation.CreatedOn = DateTime.Now;
                reservation.IsDeleted = false;
                reservation.IsFinished = false;
                saintUnits.HealthReservations.Add(reservation);
                saintUnits.Complete();
                return View("GetReservationDetails", reservation);
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
            //var reservations = saintUnits.HealthReservations.Find(x => x.ReservationNumber == reservation.ReservationNumber && x.IsDeleted == false).ToList();
            //if (reservations.Count > 1)
            //    ModelState.AddModelError("ReservationNumber", "تم أدخال رقم الحجز من قبل");

            //reservations = saintUnits.HealthReservations.Find(x => x.NationalID == reservation.NationalID && x.IsDeleted == false).ToList();
            //if (reservations.Count > 1)
            //    ModelState.AddModelError("NationalID", "تم أدخال الرقم القومى من قبل");

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
            reservation.IsDeleted = true;
            reservation.UpdatedBy = User.Identity.Name;
            reservation.UpdatedOn = DateTime.Now;
            saintUnits.HealthReservations.Update(reservation);
            saintUnits.Complete();
            return Json(data: new { success = true, message = "Reservation has been deleted successfully" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetReservationDetails(CovidReservation reservation)
        {
            CovidReservation reservationData = saintUnits.HealthReservations.Find(x => x.NationalID == reservation.NationalID).FirstOrDefault();
            return View(reservationData);
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