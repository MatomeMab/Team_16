using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nazox.Models.QuotationViewModel
{
    public class ClientQuotationClass
    {
        public Quotation quotationList { get; set; }
        public QuotationRequest QuotationRequestList { get; set; }
        public QuotationLine QuotationLineList { get; set; }
        public QuotationRequestLine QuotationRequestLineList { get; set; }
    }

    public class employeeVM
    {
        public Employee employee { get; set; }
        public EmployeeStatu EmployeeStatu { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }

}