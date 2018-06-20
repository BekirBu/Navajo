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
    public class RolesControllerTest
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
        public void GetAllRolesSuccess()
        {
            var controller = new RolesController();

            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<RoleModel>>)response;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void GetRoleSuccess()
        {
            var controller = new RolesController();
            var response = controller.GetById("UX");
            var result = (OkNegotiatedContentResult<RoleModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DeleteRoleSuccess()
        {
            Role r = new Role()
            {
                Id = "QA",
                Name = "Quality assurance",
                Type = RoleType.TeamRole,
                Hrate = 30,
                Mrate = 4500
            };

            unit.Roles.Insert(r);
            unit.Save();

            var controller = new RolesController();
            var response = controller.Delete(r.Id);
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostRoleSuccess()
        {
            Role r = new Role()
            {
                Id = "QA1",
                Name = "Quality assurance",
                Type = RoleType.TeamRole,
                Hrate = 30,
                Mrate = 4500
            };

            var controller = new RolesController();
            var response = controller.Post(r);
            var result = (OkNegotiatedContentResult<RoleModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void PutRoleSuccess()
        {
            Role r = new Role()
            {
                Id = "QA1",
                Name = "Quality assurance",
                Type = RoleType.TeamRole,
                Hrate = 30,
                Mrate = 4500
            };

            var controller = new RolesController();
            var response = controller.Put(r, "QA1");
            var result = (OkNegotiatedContentResult<RoleModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
