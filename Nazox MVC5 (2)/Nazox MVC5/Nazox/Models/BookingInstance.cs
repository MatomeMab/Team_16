//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nazox.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookingInstance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookingInstance()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public int BookingInstance_ID { get; set; }
        public Nullable<int> TrackingStatus_ID { get; set; }
        public Nullable<int> InspectionItem_ID { get; set; }
        public Nullable<int> BookingStatus_ID { get; set; }
        public Nullable<int> Booking_ID { get; set; }
        public Nullable<int> Employee_ID { get; set; }
        public Nullable<int> Truck_ID { get; set; }
        public Nullable<System.DateTime> BookingInstanceDate { get; set; }
        public string BookingInstanceDescription { get; set; }
        public Nullable<int> DateOrTimeSlotOrDriver_ID { get; set; }
    
        public virtual Booking Booking { get; set; }
        public virtual BookingStatu BookingStatu { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual InspectionItem InspectionItem { get; set; }
        public virtual TrackingStatu TrackingStatu { get; set; }
        public virtual Truck Truck { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual DateOrTimeSlotOrDriver DateOrTimeSlotOrDriver { get; set; }
    }
}