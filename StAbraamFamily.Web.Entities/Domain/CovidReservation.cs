//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StAbraamFamily.Web.Entities.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class CovidReservation
    {
        public int ReservationID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FourthName { get; set; }
        public string FullName { get; set; }
        public string ReservationNumber { get; set; }
        public string NationalID { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsFinished { get; set; }
        public string UpdatedBy { get; set; }
        public string MobileNumber { get; set; }
    }
}