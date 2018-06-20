using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models.ReportsModel
{
    public class ProjectInvoiceModel
    {
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public List<RoleInvoiceModel> Roles { get; set; }
        public string MailBody { get; set; }
        public string CustomerEmail { get; set; }
    }
}