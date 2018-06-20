using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class RoleDetailsModel : RoleModel
    {
        public virtual ICollection<EngagementModel> Engagements { get; set; }
        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}