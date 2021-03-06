﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class CompanyReport
    {
        public int NumEmployees { get; set; }
        public int NumProjects { get; set; }
        public int TotalHours { get; set; }
        public int MaxPossibleTotalHours { get; set; }

        public int MissingEntries { get; set; }
        //pto missing also

        public decimal PMUtilization { get; set; }
        public int PMCount { get; set; }

        public decimal DEVUtilization { get; set; }
        public int DEVCount { get; set; }

        public decimal QAUtilization { get; set; }
        public int QACount { get; set; }

        public decimal UIUXUtilization { get; set; }
        public int UIUXCount { get; set; }

        public List<CompanyReportTeams> OvertimeHoursTeams { get; set; }
        public List<CompanyReportProjects> RevenueProjects { get; set; }
       
    }
}