using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;

namespace TimeKeeperTest
{
    [TestClass]
    public class DeleteDayTest
    {
        [TestMethod]
        public void CheckIfDayDeleted()
        {
            TimeKeeperContext context = new TimeKeeperContext();

            Employee emp1 = new Employee()
            {
                FirstName = "Hamida",
                LastName = "Hamidovic",
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

            context.Days.Remove(d);
            context.SaveChanges();

            d = context.Days.FirstOrDefault(x => x.Employee.FirstName == emp1.FirstName);
            Assert.IsNull(d);
        }
    }
}
