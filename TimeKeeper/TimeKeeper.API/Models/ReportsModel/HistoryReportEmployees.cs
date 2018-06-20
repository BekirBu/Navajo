using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class HistoryReportEmployees
    {
        public HistoryReportEmployees()
        {
            SumList = new List<HistoryReportTotal>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? TotalHours { get; set; }

        public List<HistoryReportTotal> SumList { get; set; }

    }
}