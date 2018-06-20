using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class AnnualReport
    {
        public AnnualReport()
        {
            MonthlyHours = new decimal[12];
        }
        public string ProjectName { get; set; }
        public decimal TotalHours { get; set; }
        public decimal[] MonthlyHours { get; set; }
    }
}