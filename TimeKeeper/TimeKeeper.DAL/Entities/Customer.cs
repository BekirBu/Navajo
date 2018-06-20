using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.DAL
{
    public enum StatusCustomer
    {
        Prospect,
        Client
    }

    public class Customer : BaseClass<int>
    {

        public Customer()
        {
            Projects = new List<Project>();

        }

        [MaxLength(30)]
        public string Name { get; set; }

        //max size
        public string Image { get; set; }
        public string Monogram { get; set; }

        [MaxLength(30)]
        public string Contact { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }
        
        [MaxLength(25)]
        public string Phone { get; set; }

        public Address Address { get; set; }

        //can have more projects
        public virtual ICollection<Project> Projects { get; set; }

        //Enum
        public StatusCustomer StatusCustomer { get; set; }
    }
}
