using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class PersonalReport
    {
        public EmployeeModel Employee { get; set; }
        public List<PersonalReportDays> Days { get; set; }

        public decimal TotalHours { get; set; }
        public decimal OvertimeHours { get; set; }
        public int MissingEntries { get; set; }
        public decimal Utilization { get; set; }

        public int WorkingDays { get; set; }
        public int VacationDays { get; set; }
        public int BusinessAbscenceDays { get; set; }
        public int PublicHolidayDay { get; set; }
        public int SickLeavesDays { get; set; }
        public int ReligiousDays { get; set; }
        public int OtherDays { get; set; }
        public int WorkingDaysInMonth { get; set; }
    }
}