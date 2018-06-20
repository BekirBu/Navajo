using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class RoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Hrate { get; set; }
        public decimal Mrate { get; set; }

        //public virtual ICollection<EngagementModel> Engagements { get; set; }
        //public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}