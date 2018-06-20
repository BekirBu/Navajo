using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using System.Web.Http.Results;

namespace TimeKeeperTest
{
    [TestClass]
    public class TeamsControllerTest
    {
        [TestMethod]
        public void GetAllTeamsSuccess()
        {
            var controller = new TeamsController();

            var response = controller.Get("A");

            var result = (OkNegotiatedContentResult<TeamModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
