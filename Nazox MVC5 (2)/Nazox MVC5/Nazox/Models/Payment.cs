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
    
    public partial class Payment
    {
        public int Payment_ID { get; set; }
        public int PaymentType_ID { get; set; }
        public Nullable<int> Rental_ID { get; set; }
        public int BookingInstance_ID { get; set; }
        public Nullable<int> Client_ID { get; set; }
        public int Document_ID { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountDue { get; set; }
        public System.DateTime DatePaid { get; set; }
        public Nullable<int> PaymentStatus_ID { get; set; }
    
        public virtual BookingInstance BookingInstance { get; set; }
        public virtual Client Client { get; set; }
        public virtual DocumentFile DocumentFile { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual PaymentStatu PaymentStatu { get; set; }
        public virtual RentalAgreement RentalAgreement { get; set; }
    }
}
