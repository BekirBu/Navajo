using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class MonthlyReport
    {
        public EmployeeModel Employee { get; set; }
        public List<MonthlyReportProjects> Projects { get; set; }

        public decimal TotalWorkingHours { get; set; }
    }
}