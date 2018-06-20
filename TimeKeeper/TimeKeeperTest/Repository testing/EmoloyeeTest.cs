using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;
using System.Linq;
using System.Data.Entity.Migrations;

namespace TimeKeeperTest
{
    /// <summary>
    /// Summary description for EmoloyeeTest
    /// </summary>
    [TestClass]
    public class EmoloyeeTest
    {
        [TestMethod]
        public void CheckEmployee()
        {
            //Arrange
            TimeKeeperContext context = new TimeKeeperContext();

            Employee emp1 = new Employee()
            {
                FirstName = "Esma",
                LastName = "Holjan",
                Position = context.Roles.Find("DEV")
            };

            Employee emp2 = new Employee()
            {
                FirstName = "Bekac",
                LastName = "Beks",
                Position = context.Roles.Find("DEV"),

            };

            //Act
            context.Employees.Add(emp1);
            context.Employees.Add(emp2);
            context.SaveChanges();

            //Assert
            Assert.AreEqual("Esma", emp1.FirstName);
            Assert.AreEqual("Beks", emp2.LastName);




        }
    }
}
