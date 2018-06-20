using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class CompanyReportTeams
    {
        public string TeamName { get; set; }
        public decimal OvertimeHours { get; set; }
        public int TeamMissingHours { get; set; }
        public decimal TeamHours { get; set; }
        public double Utilization { get; set; }
    }
}