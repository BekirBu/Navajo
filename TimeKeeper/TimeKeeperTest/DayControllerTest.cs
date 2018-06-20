using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using System.Web.Http.Results;
using TimeKeeper.DAL;

namespace TimeKeeperTest
{
    [TestClass]
    public class DayControllerTest
    {
        [TestMethod]
        public void GetAllDaysSuccess()
        {
            TimeKeeperContext context = new TimeKeeperContext();

            Employee emp1 = new Employee()
            {
                FirstName = "Testera",
                LastName = "Testerovic",
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

            var controller = new DaysController();

            var response = controller.Get(1);
            var result = (OkNegotiatedContentResult<DayModel>)response;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

        }
    }
}
