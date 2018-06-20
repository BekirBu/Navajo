using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeperTest
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void CheckProject()
        {
            TimeKeeperContext context = new TimeKeeperContext();

            //Arrange
            Team t = new Team()
            {
                Id = "n01",
                Image = null,
                Name = "Navajo team"
            };

            context.Teams.Add(t);
            context.SaveChanges();

            Address add = new Address
            {
                City = "Sarajevo",
                Name = "Bosmal",
                ZipCode = "71000"
            };

            //Customer c = new Customer()
            //{
            //    Name = "Mistral Technologies",
            //    Image = null,
            //    Monogram = null,
            //    Contact = "Sulejman Catibusic",
            //    Email = "info@mistral.ba",
            //    Phone = "+38761200333",
            //    StatusCustomer = StatusCustomer.Client,
            //    Address = add
            //};

            //context.Customers.Add(c);
            //context.SaveChanges();

            Project p = new Project()
            {
                Name = "TimeKeeper",
                Description = "Application for time tracking",
                Amount = 1000,
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                StatusProject = StatusProject.InProgress,
                Pricing = Pricing.HourlyRate,
                //Customer = c,
                Team = t
            };

            //Act
            context.Projects.Add(p);
            context.SaveChanges();

            //Assert
            Assert.AreEqual("TimeKeeper", p.Name);
            //Assert.AreEqual("Mistral Technologies", p.Customer.Name);
            Assert.AreEqual("Navajo team", p.Team.Name);

        }
    }
}
