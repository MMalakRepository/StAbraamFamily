using StAbraamFamily.Web.Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StAbraamFamily.Models
{
    public class ServantModel
    {
        [Required(ErrorMessage = "برجاء أدخال الأسم")]
        public string Name { get; set; }
        [Required(ErrorMessage = "برجاء أدخال رقم الهاتف")]
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "برجاء أدخال أسم أب الأعتراف")]
        public string ConfessionFR { get; set; }
        [Required(ErrorMessage = "برجاء أدخال رقم هاتف أب الأعتراف")]
        public string ConfessionFRNumber { get; set; }
        public string ConfessionFRChurch { get; set; }
        public string SpecialStudies { get; set; }
        public string Job { get; set; }
        public string Studying { get; set; }
        public string PreviousServices { get; set; }
        public string Readings { get; set; }
        [Required(ErrorMessage = "برجاء أختيار الخدمة")]
        public string[] Services { get; set; }
        public string ServiceName { get; set; }

    }
}