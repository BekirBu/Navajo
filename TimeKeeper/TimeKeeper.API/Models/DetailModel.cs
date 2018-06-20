using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class DetailModel 
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Hours { get; set; }
        public BaseModel Project { get; set; }
        public bool Deleted { get; set; }
    }
}