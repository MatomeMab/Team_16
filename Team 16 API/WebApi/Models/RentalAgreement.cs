//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RentalAgreement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RentalAgreement()
        {
            this.BookingInstances = new HashSet<BookingInstance>();
            this.Licenses = new HashSet<License>();
            this.Payments = new HashSet<Payment>();
        }
    
        public int Rental_ID { get; set; }
        public int Truck_ID { get; set; }
        public int RentalStatus_ID { get; set; }
        public int Client_ID { get; set; }
        public string Description { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public int Inspection_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingInstance> BookingInstances { get; set; }
        public virtual Client Client { get; set; }
        public virtual Inspection Inspection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<License> Licenses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual RentalStatu RentalStatu { get; set; }
        public virtual Truck Truck { get; set; }
    }
}