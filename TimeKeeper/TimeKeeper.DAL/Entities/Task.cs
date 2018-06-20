using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL
{
    public class Task : BaseClass<int>
    {
        //FK:
        public virtual Day Day { get; set; }
        public virtual Project Project { get; set; }

        //max size 
        public string Description { get; set; }

        public decimal Hours { get; set; }
    }
}
