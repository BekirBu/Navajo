﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace TimeKeeper.Notification
{
    public partial class TimeService : ServiceBase
    {
        System.Timers.Timer delayTime;
        int count;

        public TimeService()
        {
            InitializeComponent();
            delayTime = new System.Timers.Timer();
            delayTime.Elapsed += new ElapsedEventHandler(WorkProcess);
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Service started!");
            delayTime.Enabled = true;

        }

        protected override void OnStop()
        {
            Console.WriteLine("Service stoped!");
            delayTime.Enabled = false;
        }

        public void WorkProcess(object sender, ElapsedEventArgs e)
        {
            //var conString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient();

            IMongoDatabase dbBase = client.GetDatabase("TimeKeeperStorage");
            var messages = dbBase.GetCollection<MailContent>("Mails");

            List<MailContent> contents = messages.Find(Builders<MailContent>.Filter.Empty).ToList();

            if (contents.Count > 0)
            {

                foreach (var content in contents)
                {
                    content.MailSubject = "Notification for " + content.ReceiverMailAddress;
                    content.ReceiverMailAddress = "navajotimekeeper@gmail.com";

                    MailMessage mailMessage = new MailMessage("navajotimekeeper@gmail.com", content.ReceiverMailAddress)
                    {
                        Body = content.MailBody,
                        Subject = content.MailSubject
                    };
                    //SmtpClient smtpClient = new SmtpClient();
                    SmtpClient smtpClient = new SmtpClient
                    {
                        Port = 587,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Timeout = 10000,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("navajotimekeeper@gmail.com", "timekeeper")
                    };
                    smtpClient.Send(mailMessage);

                    messages.DeleteOne(Builders<MailContent>.Filter.Eq("_id", content.Id));
                    Thread.Sleep(2500);
                }
            }

        }


        public void WorkProcessInvoice(object sender, ElapsedEventArgs e)
        {
            MongoClient client = new MongoClient();

            IMongoDatabase dbBase = client.GetDatabase("TimeKeeperStorage");
            var messages = dbBase.GetCollection<MailContent>("Mails");

            List<MailContent> contents = messages.Find(Builders<MailContent>.Filter.Empty).ToList();

            if (contents.Count > 0)
            {

                foreach (var content in contents)
                {
                    content.MailSubject = "Notification for " + content.ReceiverMailAddress;
                    content.ReceiverMailAddress = "navajotimekeeper@gmail.com";

                    MailMessage mailMessage = new MailMessage("navajotimekeeper@gmail.com", content.ReceiverMailAddress)
                    {
                        Body = content.MailBody,
                        Subject = content.MailSubject
                    };
                    SmtpClient smtpClient = new SmtpClient
                    {
                        Port = 587,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Timeout = 10000,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("navajotimekeeper@gmail.com", "timekeeper")
                    };
                    smtpClient.Send(mailMessage);

                    messages.DeleteOne(Builders<MailContent>.Filter.Eq("_id", content.Id));
                    Thread.Sleep(2500);
                }
            }

        }
    }
}
