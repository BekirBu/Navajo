using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public class Category : BaseClass<int>
    {
        public Category()
        {
            Days = new List<Day>();
        }
        
        //max description size
        public string Description { get; set; }

        //more days
        public virtual ICollection<Day> Days { get; set; }
    }
}
