using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Salary { get; set; }
        public RoleModel Position { get; set; }
        public string RoleId { get; set; }
        public int StatusEmployee { get; set; }

        public ICollection<BaseModel> Projects { get; set; }
        //public ICollection<DayModel> Days { get; set; }
    }
}