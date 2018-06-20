using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using System.Web;
using System.IO;
using System.Collections.Generic;
using TimeKeeper.DAL.Repositories;
using TimeKeeper.DAL;

namespace TimeKeeperTest.Controllers_testing
{
    [TestClass]
    public class TeamsControllerTest
    {
        UnitOfWork unit = new UnitOfWork();

        [TestInitialize]
        public void Initialize()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter()));
        }

        [TestMethod]
        public void GetAllTeamsSuccess()
        {
            var controller = new TeamsController();

            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<TeamModel>>)response;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void GetTeamSuccess()
        {
            var controller = new TeamsController();
            var response = controller.GetById("A");
            var result = (OkNegotiatedContentResult<TeamModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DeleteTeamSuccess()
        {
            Team t = new Team()
            {
                Name = "Fico",
                Id = "F",
                Image = "F",
                Description = "Auto fico Team"
            };

            unit.Teams.Insert(t);
            unit.Save();

            var controller = new TeamsController();
            var response = controller.Delete(t.Id);
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostTeamSuccess()
        {
            Team t = new Team()
            {
                Name = "Buba",
                Id = "Bu",
                Image = "B",
                Description = "Auto buba Team"
            };

            var controller = new TeamsController();
            var response = controller.Post(t);
            var result = (OkNegotiatedContentResult<TeamModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void PutTeamSuccess()
        {
            Team t = new Team()
            {
                Name = "Buba VW",
                Id = "Bu",
                Image = "B",
                Description = "Auto buba Team"
            };

            var controller = new TeamsController();
            var response = controller.Put(t, "Bu");
            var result = (OkNegotiatedContentResult<TeamModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

    }
}
