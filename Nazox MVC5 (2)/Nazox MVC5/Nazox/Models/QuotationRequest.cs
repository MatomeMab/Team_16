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
    
    public partial class QuotationRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuotationRequest()
        {
            this.QuotationLines = new HashSet<QuotationLine>();
            this.QuotationRequestLines = new HashSet<QuotationRequestLine>();
        }
    
        public int QuotationRequest_ID { get; set; }
        public int Client_ID { get; set; }
        public System.DateTime QuotationReqDate { get; set; }
        public string QuotationReqDescription { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuotationLine> QuotationLines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuotationRequestLine> QuotationRequestLines { get; set; }
    }
}
