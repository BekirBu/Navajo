using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL
{
    public enum StatusEmployee
    {
        Trial,
        Active,
        Leaver
    }

    public class Employee : BaseClass<int>
    {

        public Employee()
        {
            Engagement = new List<Engagement>();
            Days = new List<Day>();
        }

        public string Password { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        //max size
        public string Image { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }


        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BeginDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndDate { get; set; }

        public decimal Salary { get; set; }

        //Foreign Key
        public virtual Role Position { get; set; }
        public string RoleId { get; set; }

        public virtual ICollection<Engagement> Engagement { get; set; }
        public virtual ICollection<Day> Days { get; set; }

        //ENUM
        public StatusEmployee StatusEmployee { get; set; }

    }
}
