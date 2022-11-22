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

        //[MaxLength(20, ErrorMessage = "رقم حجز وزارة الصحة يحتوى على 20 رقم فقط ")]
        //[MinLength(20, ErrorMessage = "برجاء أدخال 20 رقم حجز وزارة الصحة")]
        //[Required(ErrorMessage = "برجاء أدخال رقم حجز وزارة الصحة")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "برجاء أدخال الرقم باللغة الأنجليزية فقط")]
        public string ReservationNumber { get; set; }

        //[MaxLength(14, ErrorMessage = "رقم البطاقة يحتوى على 14 رقم فقط ")]
        //[MinLength(14, ErrorMessage = "برجاء أدخال 14 رقم للبطاقة")]
        //[Required(ErrorMessage = "برجاء أدخال رقم البطاقة")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "برجاء أدخال الرقم باللغة الأنجليزية فقط")]
        public string NationalID { get; set; }

        [MaxLength(11, ErrorMessage = "رقم الموبايل يحتوى على 11 رقم فقط ")]
        [MinLength(11, ErrorMessage = "برجاء أدخال 11 رقم الموبايل")]
        [Required(ErrorMessage = "برجاء أدخال رقم الموبايل")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "برجاء أدخال الرقم باللغة الأنجليزية فقط")]
        public string MobileNumber { get; set; }

    }
}
