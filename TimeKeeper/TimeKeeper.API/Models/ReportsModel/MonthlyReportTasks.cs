using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class MonthlyReportTasks
    {
        public string Description { get; set; }
        public decimal Hours { get; set; }
    }
}