//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupportSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupportSystemStatuses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupportSystemStatuses()
        {
            this.SupportSystemMain = new HashSet<SupportSystemMain>();
        }
    
        public System.Guid Id { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupportSystemMain> SupportSystemMain { get; set; }
    }
}
