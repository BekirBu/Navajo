using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class HistoryReportTotal
    {
        public int Year { get; set; }
        public decimal? Sum { get; set; }
    }
}