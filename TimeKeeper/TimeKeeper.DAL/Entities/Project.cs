using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL
{
    public enum StatusProject
    {
        InProgress,
        OnHold,
        Finished,
        Canceled
    }

    public enum Pricing
    {
        HourlyRate,
        PerCapitaRate,
        FixedRate,
        NotBillable
    }

    public class Project : BaseClass<int>
    {

        public Project()
        {
            Tasks = new List<Task>();
        }

        //max size
        public string Monogram { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        //max size
        public string Description { get; set; }

        public decimal Amount { get; set; }

        //FK:
        //One project has one customer
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        

        //FK
        //One project has one team
        public string TeamId { get; set; }
        public virtual Team Team { get; set; }
        

        //One project has 0 ore more tasks
        public virtual ICollection<Task> Tasks { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        //ENUMS:
        public StatusProject StatusProject { get; set; }
        public Pricing Pricing { get; set; }
    }
}
