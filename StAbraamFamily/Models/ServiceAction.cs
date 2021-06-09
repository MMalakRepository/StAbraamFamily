//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StAbraamFamily.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceAction
    {
        public int ID { get; set; }
        public Nullable<int> ServantID { get; set; }
        public string UserID { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public string Notes { get; set; }
        public Nullable<int> ActionTypeID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> FamilyID { get; set; }
        public Nullable<int> PersonID { get; set; }
        public Nullable<int> HospitalID { get; set; }
        public Nullable<int> ClinicID { get; set; }
        public Nullable<int> MedicalContractID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Cost { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<int> MedicalServiceTypeID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual Family Family { get; set; }
        public virtual Hospital Hospital { get; set; }
        public virtual Person Person { get; set; }
        public virtual Servant Servant { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual MedicalService MedicalService { get; set; }
    }
}
