using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class MonthlyReportProjects
    {
        public string Name { get; set; }
        public string Monogram { get; set; }
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Pricing { get; set; }
        public decimal Amount { get; set; }
        public decimal Hours { get; set; }

        public virtual List<MonthlyReportTasks> Tasks { get; set; }
    }
}