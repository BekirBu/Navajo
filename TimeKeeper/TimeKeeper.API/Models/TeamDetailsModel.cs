using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class TeamDetailsModel : TeamModel
    {
        public ICollection<EngagementModel> Members { get; set; }
        public ICollection<ProjectModel> Projects { get; set; }
    }
}