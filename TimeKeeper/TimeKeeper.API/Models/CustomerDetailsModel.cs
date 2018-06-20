using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class CustomerDetailsModel : CustomerModel
    {
        public virtual ICollection<ProjectModel> Projects { get; set; }
    }
}