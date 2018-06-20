using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL
{
    public class Team : BaseClass<string>
    {

        public Team()
        {
            Engagements = new List<Engagement>();
            Projects = new List<Project>();
        }

        [MaxLength(30)]
        public string Name { get; set; }

        //max size
        public string Image { get; set; }
        public string Description { get; set; }

        //one team has more engagements
        public virtual ICollection<Engagement> Engagements { get; set; }

        //one team has one project
        public virtual ICollection<Project> Projects { get; set; }
    }
}
