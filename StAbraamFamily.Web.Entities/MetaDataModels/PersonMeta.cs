using System;
using System.ComponentModel.DataAnnotations;

namespace StAbraamFamily.Web.Entities.Domain
{
    [MetadataType(typeof(PersonMetaData))]
    public partial class Person
    {

    }

    public class PersonMetaData
    {
        //[MaxLength(14,ErrorMessage ="رقم البطاقة يحتوى على 14 رقم فقط ")]
        //[MinLength(14,ErrorMessage ="برجاء أدخال 14 رقم للبطاقة")]
        //[Required(ErrorMessage ="برجاء أدخال رقم البطاقة")]
        public string NationalID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> EntryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DateOfDeath { get; set; }


        //[Required(ErrorMessage = "برجاء أدخال الدخل الشهرى")]
        public decimal Salary { get; set; }
        //[Required(ErrorMessage = "برجاء أدخال الحالة الأجتماعية")]

        public bool Status { get; set; }
    }
}