using System;
using System.Data.Entity.Migrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;

namespace TimeKeeperTest
{
    [TestClass]
    public class UpdateTest
    {
        [TestMethod]
        public void CheckIfUpdated()
        {
            TimeKeeperContext context = new TimeKeeperContext();
            Employee emp1 = new Employee()
            {
                FirstName = "Bekir",
                LastName = "Beks",
                Position = context.Roles.Find("DEV"),

            };

            context.Employees.AddOrUpdate(emp1);
            context.SaveChanges();

            Assert.AreEqual("Bekir", emp1.FirstName);
        }
    }
}
