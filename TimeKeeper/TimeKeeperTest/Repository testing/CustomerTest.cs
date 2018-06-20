using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeperTest
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void CheckCustomer()
        {
            TimeKeeperContext context = new TimeKeeperContext();

            Address add = new Address();
            add.City = "Sarajevo";
            add.Name = "Bosmal";
            add.ZipCode = "71000";

            Customer c = new Customer()
            {
                Name = "Mistral Technologies",
                Image = null,
                Monogram = null,
                Contact = "Sulejman Catibusic",
                Email = "info@mistral.ba",
                Phone = "+38761200333",
                StatusCustomer = StatusCustomer.Client,
                Address = add
            };

            context.Customers.Add(c);
            context.SaveChanges();

            Assert.AreEqual("Sulejman Catibusic", c.Contact);
        }
    }
}
