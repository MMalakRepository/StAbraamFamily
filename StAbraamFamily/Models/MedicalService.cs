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
    
    public partial class MedicalService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicalService()
        {
            this.MedicalContracts = new HashSet<MedicalContract>();
        }
    
        public int ID { get; set; }
        public string MedicalService1 { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalContract> MedicalContracts { get; set; }
    }
}