using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<string> Teams { get; set; }
        //public string Token { get; set; } //jer ga donosimo kroz header
        public string Provider { get; set; }
        public List<string> TeamLeadTo { get; set; }
    }
}