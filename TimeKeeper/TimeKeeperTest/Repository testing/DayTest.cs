using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;
using System.Linq;

namespace TimeKeeperTest
{
    [TestClass]
    public class DayTest
    {
        [TestMethod]
        public void CheckDay()
        {
            TimeKeeperContext context = new TimeKeeperContext();

            Employee emp1 = new Employee()
            {
                FirstName = "Tester",
                LastName = "Testerovski",
                Position = context.Roles.Find("DEV")
            };

            Day d = new Day()
            {
                Date = DateTime.Now,
                Hours = 8m,
                Type = DayType.WorkingDay,
                Employee = emp1
            };

            context.Employees.Add(emp1);
            context.Days.Add(d);
            context.SaveChanges();

            Assert.AreEqual(8, d.Hours);
        }
    }
}
