
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL
{
    public class Engagement : BaseClass<int>
    {
        public decimal Hours { get; set; }

        //Engagement can have one emplyoee, team, role
        //FK:
        public virtual Employee Employee { get; set; }
        public virtual Team Team { get; set; }
        public virtual Role Role { get; set; }
    }
}