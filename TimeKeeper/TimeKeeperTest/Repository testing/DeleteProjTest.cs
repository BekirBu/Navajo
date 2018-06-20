using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeperTest
{
    [TestClass]
    public class DeleteProjTest
    {
        [TestMethod]
        public void CheckIfProjDeleted()
        {

            TimeKeeperContext context = new TimeKeeperContext();

            Team t = new Team()
            {
                Id = "n03",
                Image = null,
                Name = "Navajo team"
            };

            Address add = new Address
            {
                City = "Sarajevo",
                Name = "Bosmal",
                ZipCode = "71000"
            };

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

            Project p = new Project()
            {
                Name = "Testera",
                Description = "Application for wood cutting",
                Amount = 1000,
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                StatusProject = StatusProject.InProgress,
                Pricing = Pricing.HourlyRate,
                Customer = c,
                Team = t
            };

            //Act
            context.Projects.Add(p);
            context.SaveChanges();

            context.Projects.Remove(p);
            context.SaveChanges();

            p = context.Projects
                      .FirstOrDefault(x => x.Name == "Testera"  );
                Assert.IsNull(p);
        }
    }
}
