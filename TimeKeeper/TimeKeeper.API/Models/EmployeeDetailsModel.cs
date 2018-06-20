using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class EmployeeDetailsModel : EmployeeModel
    {
        public ICollection<EngagementModel> Engagements { get; set; }
        public ICollection<CalendarModel> Days { get; set; }
    }
}