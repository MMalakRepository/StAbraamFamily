using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StAbraamFamily.Web.Entities.Domain
{
    [MetadataType(typeof(CovidReservationMetaData))]
    public partial class CovidReservation
    {

    }
    public class CovidReservationMetaData
    {
        [Required(ErrorMessage = "برجاء أدخال الأسم الأول")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "برجاء أدخال الأسم الثانى")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "برجاء أدخال الأسم الثالث")]
        public string ThirdName { get; set; }

        [Required(ErrorMessage = "برجاء أدخال الأسم الرابع")]
        public string FourthName { get; set; }
        public string FullName { get; set; }

        [MaxLength(20, ErrorMessage = "رقم حجز وزارة الصحة يحتوى على 20 رقم فقط ")]
        [MinLength(20, ErrorMessage = "برجاء أدخال 20 رقم حجز وزارة الصحة")]
        [Required(ErrorMessage = "برجاء أدخال رقم حجز وزارة الصحة")]
        [Remote("IsReservationNumberAvailable", "HealthReservations", ErrorMessage = "تم أدخال هذا الحجز من قبل")]
        public string ReservationNumber { get; set; }

        [MaxLength(14, ErrorMessage = "رقم البطاقة يحتوى على 14 رقم فقط ")]
        [MinLength(14, ErrorMessage = "برجاء أدخال 14 رقم للبطاقة")]
        [Required(ErrorMessage = "برجاء أدخال رقم البطاقة")]
        [Remote("IsNationalIDAvailable", "HealthReservations", ErrorMessage = "تم أدخال رقم البطاقة من قبل")]
        public string NationalID { get; set; }

    }
}
