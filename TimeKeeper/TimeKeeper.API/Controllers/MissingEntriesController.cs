using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TimeKeeper.DAL;
using TimeKeeper.API;
using TimeKeeper.API.Helper.Reports;
using MongoDB.Driver;
using MongoDB.Bson;
using TimeKeeper.API.Models;
using TimeKeeper.MailDB;
using TimeKeeper.MailDB.Entities;

namespace TimeKeeper.API.Controllers
{
    public class MissingEntriesController : BaseController
    {
        [System.Web.Http.Route("api/missingEntries/{year}/{month}")]
        public IHttpActionResult Get(int year, int month)
        {
            return Ok(TimeUnit.GetMissingEntries(year, month, TimeFactory));

        }

        [System.Web.Http.Route("api/missingEntries")]
        public IHttpActionResult NotifyForMissingEntries([FromBody] List<MissingEntriesModel> employees)
        {
            try
            {
                MailStorageService mailService = new MailStorageService();
                foreach (var e in employees)
                {
                    
                    //var employee = TimeUnit.Employees.Get(x => x.Id == e.Employee.Id).FirstOrDefault();
                    //var mailBody = e.MailBody(employee);
                    var mailBody = e.MailBody;

                    mailService.StoreMails(new MailContent()
                    {
                        ReceiverMailAddress = e.Employee.Email,
                        MailBody = mailBody,
                        MailSubject = "Missing Entries Notification",
                        DateCreated = DateTime.Now
                    });

                }
                return Ok("Successifully sent messages.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }

    //    public IHttpActionResult Post([FromBody] List<MissingEntriesModel> model)
    //    {
    //        var conString = "mongodb://localhost:27017";

    //        var client = new MongoClient(conString);

    //        var DB = client.GetDatabase("EmailMissingEntries");
    //        var collection = DB.GetCollection<BsonDocument>("Test");

    //        foreach (var employee in model)
    //        {
    //            var document = new BsonDocument
    //        {
    //            {"Name" , employee.Employee.FirstName },
    //            {"Email", employee.Employee.Email }

    //        };
    //            var arr = new BsonArray();
    //            foreach (var entry in employee.MissingDays)
    //            {
    //                arr.Add(new BsonDocument("Day:", entry));

    //            }
    //            document.Add("MissingEntries:", arr);
    //            collection.InsertOne(document);
    //        }
    //        return Ok(model);
    //    }
}

