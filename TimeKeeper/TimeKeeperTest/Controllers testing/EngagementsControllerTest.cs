using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Repositories;

namespace TimeKeeperTest.Controllers_testing
{
    [TestClass]
    public class EngagementsControllerTest
    {
        UnitOfWork unit = new UnitOfWork();

        [TestInitialize]
        public void Initialize() => HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter()));

        [TestMethod]
        public void GetAllEngagementsSuccess()
        {
            var controller = new EngagementsController();

            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<EngagementModel>>)response;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void GetEngagementSuccess()
        {
            var controller = new EngagementsController();
            var response = controller.GetById(1);
            var result = (OkNegotiatedContentResult<EngagementModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DeleteEngagementSuccess()
        {
            Engagement e = new Engagement()
            {
                Hours = 40,
                Employee = unit.Employees.Get(1),
                Team = unit.Teams.Get("A"),
                Role = unit.Roles.Get("SD")
            };

            unit.Engagements.Insert(e);
            unit.Save();

            var controller = new EngagementsController();
            var response = controller.Delete(e.Id);
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostEngagementSuccess()
        {
            Engagement e = new Engagement()
            {
                Hours = 40,
                Employee = new Employee()
                {
                    FirstName = "Alena",
                    LastName = "KOzic",
                    Email = "mail@gmail.com",
                    BirthDate = DateTime.Now.AddYears(-21),
                    StatusEmployee = StatusEmployee.Active,
                    BeginDate = DateTime.Now.AddYears(-1),
                    Salary = 2000
                },
                Team = new Team()
                {
                    Name = "Thor",
                    Id = "Th",
                    Image = "ThorImgage",
                    Description = "Thor Team"
                },
                Role = new Role()
                {
                    Id = "ACC",
                    Name = "Accountant",
                    Type = RoleType.JobTitle,
                    Hrate = 30,
                    Mrate = 4500
                }
            };

            var controller = new EngagementsController();
            var response = controller.Post(e);
            var result = (OkNegotiatedContentResult<EngagementModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void PutEngagementSuccess()
        {
            Engagement e = new Engagement()
            {
                Id = 2,
                Hours = 20,
                Employee = new Employee()
                {
                    FirstName = "Alena",
                    LastName = "KOzic",
                    Email = "mail@gmail.com",
                    BirthDate = DateTime.Now.AddYears(-21),
                    StatusEmployee = StatusEmployee.Active,
                    BeginDate = DateTime.Now.AddYears(-1),
                    Salary = 2000
                },
                Team = new Team()
                {
                    Name = "Thor",
                    Id = "Th",
                    Image = "ThorImgage",
                    Description = "Thor Team"
                },
                Role = new Role()
                {
                    Id = "LE",
                    Name = "Legal",
                    Type = RoleType.JobTitle,
                    Hrate = 30,
                    Mrate = 4500
                }
            };

            var controller = new EngagementsController();
            var response = controller.Put(e, 2);
            var result = (OkNegotiatedContentResult<EngagementModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
