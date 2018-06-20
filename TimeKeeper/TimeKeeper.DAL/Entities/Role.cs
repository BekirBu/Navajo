using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL
{
    public enum RoleType
    {
        JobTitle,
        TeamRole,
        AppRole
    }
    public class Role : BaseClass<string>
    {

        public Role()
        {
            Engagements = new List<Engagement>();
            Employees = new List<Employee>();
        }

        [MaxLength(30)]
        public string Name { get; set; }

        public RoleType Type { get; set; }
        
        public decimal Hrate { get; set; }
        public decimal Mrate { get; set; }

        //can have more employees
        public virtual ICollection<Engagement> Engagements { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}