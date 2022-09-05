using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nazox.Models
{
    public class ReportsVM
    {
        //report criteria
        public IEnumerable<SelectListItem> Employees { get; set; }
        public int SelectedEmployeeID { get; set; }
        public DateTime DateFrom{ get; set; }
        public DateTime DateTo { get; set; }

        //report data
        public Employee employee { get; set; }
        public List<IGrouping<string,ReportRecord>> results { get; set; }
        public Dictionary<string,double> chartData { get; set; }

        public class ReportRecord
        {
            public string Employee { get; set; }
        }
    }
}