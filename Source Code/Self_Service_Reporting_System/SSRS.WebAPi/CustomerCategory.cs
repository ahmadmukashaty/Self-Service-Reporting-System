//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSRS.WebAPi
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerCategory()
        {
            this.Customers = new HashSet<Customer>();
            this.SpecialDeals = new HashSet<SpecialDeal>();
        }
    
        public int ID { get; set; }
        public string CustomerCategoryName { get; set; }
        public int LastEditedBy { get; set; }
        public System.DateTime ValidFrom { get; set; }
        public System.DateTime ValidTo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpecialDeal> SpecialDeals { get; set; }
    }
}
