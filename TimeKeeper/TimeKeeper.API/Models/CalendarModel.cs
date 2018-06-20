using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Models
{
    public class CalendarModel
    {
        //public CalendarModel(BaseModel employee, int year, int month)
        public CalendarModel(int employeeId, int year, int month)
        {
            Employee = employeeId;
            Month = month;
            Year = year;
            int Limit = DateTime.DaysInMonth(year, month);
            Days = new DayModel[Limit];
            for (int i = 0; i < Limit; i++)
            {
                //dodaj employee
                Days[i] = new DayModel()
                {
                    Date = new DateTime(year, month, i + 1),
                    Hours = 0,
                    Type = 0
                };
                if (Days[i].Date >= DateTime.Today) Days[i].Type = 9;               // future
                if (Days[i].Date.DayOfWeek == DayOfWeek.Saturday ||
                    Days[i].Date.DayOfWeek == DayOfWeek.Sunday) Days[i].Type = 8;   // weekend
            }
        }
        //public BaseModel Employee { get; set; }
        public int Employee { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DayModel[] Days { get; set; }

    }
}
 