using System;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;

namespace TimeKeeperTest
{
    [TestClass]
    public class EngagementsControllerTest
    {
        [TestMethod]
        public void GetEngagementsSuccess()
        {
            TimeKeeperContext context = new TimeKeeperContext();

            Team t = new Team()
            {
                Id = "n04",
                Image = null,
                Name = "NavajoO team"
            };

            Employee emp1 = new Employee()
            {
                FirstName = "Hamo",
                LastName = "Hamic",
                Position = context.Roles.Find("DEV")
            };

            Engagement eng1 = new Engagement()
            {
                Hours = 40m,
                Employee = emp1,
                Team = t,
                Role = context.Roles.Find("SD")
            };

            context.Engagements.Add(eng1);
            context.SaveChanges();

            var controller = new EngagementsController();

            var response = controller.Get(1);
            var result = (OkNegotiatedContentResult<EngagementModel>)response;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void PostEngagementsSuccess()
        {
            Team t = new Team()
            {
                Id = "n05",
                Image = null,
                Name = "NavajoO team"
            };

            Employee emp1 = new Employee()
            {
                FirstName = "Hamo",
                LastName = "Hamic"
            };

            Role r = new Role()
            {           
                Id = "SD",
                Name = "Software Developer",
                Type = RoleType.TeamRole,
                Hrate = 30,
                Mrate = 4500
        };

            Engagement eng2 = new Engagement()
            {
                Hours = 40m,
                Team = t,
                Employee = emp1,
                Role = r
            };

            var controller = new EngagementsController();

            var response = controller.Post(eng2);
            var result = (OkNegotiatedContentResult<EngagementModel>)response;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
