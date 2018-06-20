using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public class Address
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
