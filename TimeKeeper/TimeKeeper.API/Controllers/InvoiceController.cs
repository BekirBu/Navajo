using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TimeKeeper.API.Models.ReportsModel;
using TimeKeeper.MailDB;
using TimeKeeper.MailDB.Entities;

namespace TimeKeeper.API.Controllers
{
    public class InvoiceController : BaseController
    {
        [System.Web.Http.Route("api/invoices")]
        public IHttpActionResult NotifyForMissingEntries([FromBody] ProjectInvoiceModel invoices)
        {
            try
            {
                MailStorageService mailService = new MailStorageService();
                //foreach (var invoice in invoices)
                //{
                    var mailBody = invoices.MailBody;

                    mailService.StoreMails(new MailContent()
                    {
                        ReceiverMailAddress = invoices.CustomerEmail,
                        MailBody = mailBody,
                        MailSubject = "Invoice for your project.",
                        DateCreated = DateTime.Now
                    });

                //}
                return Ok("Successifully sent messages.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}