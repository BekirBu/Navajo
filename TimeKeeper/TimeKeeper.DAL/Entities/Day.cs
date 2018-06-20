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
    public enum DayType
    {
        WorkingDay = 1,
        PublicHoliday,
        OtherAbsence,
        ReligiousDay,
        SickLeave,
        Vacation,
        BusinessAbsence
    }

    public class Day : BaseClass<int>
    {
        public Day()
        {
            Tasks = new List<Task>();
        }

        public DateTime Date { get; set; }

        //Day can have one employee
        public virtual Employee Employee { get; set; }

        public decimal Hours { get; set; }

        [MaxLength(200)]
        public string Comment { get; set; }

        //ENUM
        public DayType Type { get; set; }

        //Calendar can have more tasks
        public virtual ICollection<Task> Tasks { get; set; }

        ////Calendar can have one category
        //public virtual Category Category { get; set; }

    }
}
