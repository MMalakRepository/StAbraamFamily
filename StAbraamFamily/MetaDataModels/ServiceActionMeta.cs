using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StAbraamFamily.Models
{
    [MetadataType(typeof(ServiceActionMeta))]
    public partial class ServiceAction
    {
       

    }

    public class ServiceActionMeta
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ActionDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> EnterDate { get; set; }

    }
}