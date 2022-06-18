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
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.BookingInstances = new HashSet<BookingInstance>();
            this.DateOrTimeSlotOrDrivers = new HashSet<DateOrTimeSlotOrDriver>();
        }
    
        public int Employee_ID { get; set; }
        public int User_ID { get; set; }
        public int EmployeeType_ID { get; set; }
        public int EmployeeStatus_ID { get; set; }
        public int Title_ID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public System.DateTime DateEmployed { get; set; }
        public string PhoneNum { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencySurname { get; set; }
        public string EmergencyPhoneNum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingInstance> BookingInstances { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DateOrTimeSlotOrDriver> DateOrTimeSlotOrDrivers { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public virtual EmployeeStatu EmployeeStatu { get; set; }
        public virtual Title Title { get; set; }
        public virtual User User { get; set; }
    }
}
