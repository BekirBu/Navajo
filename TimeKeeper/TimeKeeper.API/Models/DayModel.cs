using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class DayModel 
    {
        //public DayModel(BaseModel employee)
        public DayModel()
        {
            //Employee = employee;
            Details = new DetailModel[0];
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        //public BaseModel Employee { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public int Type { get; set; }
        public decimal Hours { get; set; }
        public DetailModel[] Details { get; set; }
    }
}