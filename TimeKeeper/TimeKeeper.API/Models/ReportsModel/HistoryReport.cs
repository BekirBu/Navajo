using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class HistoryReport
    {
        public HistoryReport()
        {
            Employees = new List<HistoryReportEmployees>();
        }

        public string ProjectName { get; set; }
        public decimal Amount { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MonhtsTotal { get; set; }
        public decimal? TotalHours { get; set; }
        public List<decimal> Hours { get; set; }
        public decimal[] AnnualHours { get; set; }

        public List<HistoryReportEmployees> Employees { get; set; }
    }
}