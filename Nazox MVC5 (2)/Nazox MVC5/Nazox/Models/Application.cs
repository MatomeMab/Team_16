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
    
    public partial class Application
    {
        public int Application_ID { get; set; }
        public int Candidate_ID { get; set; }
        public int Job_ID { get; set; }
        public int Document_ID { get; set; }
        public int ApplicationStatus_ID { get; set; }
        public System.DateTime ApplicationDate { get; set; }
    
        public virtual ApplicationStatu ApplicationStatu { get; set; }
        public virtual Candidate Candidate { get; set; }
        public virtual DocumentFile DocumentFile { get; set; }
        public virtual JobListing JobListing { get; set; }
    }
}