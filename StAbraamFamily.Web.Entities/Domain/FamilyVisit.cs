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
    
    public partial class FamilyVisit
    {
        public int ID { get; set; }
        public Nullable<int> FamilyID { get; set; }
        public Nullable<int> ServantID { get; set; }
        public Nullable<System.DateTime> VisitDate { get; set; }
        public string Notes { get; set; }
    
        public virtual Family Family { get; set; }
        public virtual Servant Servant { get; set; }
    }
}