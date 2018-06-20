using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class ProjectDetailsModel : ProjectModel
    {
        public virtual ICollection<DetailModel> Tasks { get; set; }
    }
}