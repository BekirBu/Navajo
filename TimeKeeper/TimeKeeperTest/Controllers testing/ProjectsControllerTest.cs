using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repositories;

namespace TimeKeeperTest.Controllers_testing
{
    [TestClass]
    public class ProjectsControllerTest
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
        public void GetAllProjectsSuccess()
        {
            var controller = new ProjectsController();
            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<ProjectModel>>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void GetProjectSuccess()
        {
            var controller = new ProjectsController();
            var response = controller.GetById(1);
            var result = (OkNegotiatedContentResult<ProjectModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DeleteProjectSuccess()
        {
            Project p = new Project()
            {
                 Name = "Social network",
                 BeginDate = DateTime.Now.AddYears(-1),
                 Monogram = "SN",
                 Amount = 1500,
                 Customer = unit.Customer.Get(2),
                 Description = "Network for companies|",
                 Pricing = Pricing.FixedRate,
                 StatusProject = StatusProject.InProgress,
                 Team = unit.Teams.Get("B")
             };

            unit.Projects.Insert(p);
            unit.Save();

            var controller = new ProjectsController();
            var response = controller.Delete(p.Id);
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostProjectSuccess()
        {
            Project p = new Project()
            {
                Name = "New social network",
                BeginDate = DateTime.Now.AddYears(-1),
                Monogram = "SN",
                Amount = 1500,
                Customer = new Customer()
                {
                    Image = "image",
                    Monogram = "BHT",
                    Name = "BH Telekom",
                    Contact = "Rambo Amadeus",
                    Address = new Address()
                    {
                        City = "Sarajevo",
                        Name = "Bosmal",
                        ZipCode = "71000"
                    },
                    Email = "telekom@mail.com",
                    Phone = "654654654",
                    StatusCustomer = StatusCustomer.Client
                },
                Description = "Network for companies|",
                Pricing = Pricing.FixedRate,
                StatusProject = StatusProject.InProgress,
                Team = new Team()
                {
                    Name = "AlphaBetha",
                    Id = "AB",
                    Image = "A",
                    Description = "Alpha Team"
                }
            };

            var controller = new ProjectsController();
            var response = controller.Post(p);
            var result = (OkNegotiatedContentResult<ProjectModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void PutProjectSuccess()
        {
            Project p = new Project()
            {
                Id = 2,
                Name = "New modified network",
                BeginDate = DateTime.Now.AddYears(-1),
                Monogram = "SN",
                Amount = 1500,
                Customer = unit.Customer.Get(2),
                Description = "Network for companies|",
                Pricing = Pricing.FixedRate,
                StatusProject = StatusProject.InProgress,
                Team = unit.Teams.Get("B")
            };

            var controller = new ProjectsController();
            var response = controller.Put(p, 2);
            var result = (OkNegotiatedContentResult<ProjectModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
