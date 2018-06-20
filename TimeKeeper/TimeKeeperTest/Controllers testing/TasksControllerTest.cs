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
    public class TasksControllerTest
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
        public void GetAllTasksSuccess()
        {
            var controller = new TasksController();

            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<DetailModel>>)response;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void GetTaskSuccess()
        {
            var controller = new TasksController();
            var response = controller.GetById(1);
            var result = (OkNegotiatedContentResult<DetailModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DeleteTaskSuccess()
        {
            Task t = new Task()
            {
                Day = unit.Days.Get(1),
                Description = "Simple maintenance task for us",
                Hours = 1,
                Project = unit.Projects.Get(1)
            };

            unit.Tasks.Insert(t);
            unit.Save();

            var controller = new TasksController();
            var response = controller.Delete(t.Id);
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostTaskSuccess()
        {
            Task t = new Task()
            {
                Description = "desc",
                Day = new Day()
                {
                    Hours = 8,
                    Comment = "This is a working day",
                    Type = DayType.WorkingDay,
                    Date = DateTime.Now,
                    Employee = new Employee()
                    {
                        FirstName = "Anela",
                        LastName = "Hamic",
                        Email = "mail@gmail.com",
                        BirthDate = DateTime.Now.AddYears(-21),
                        StatusEmployee = StatusEmployee.Active,
                        BeginDate = DateTime.Now.AddYears(-1),
                        Salary = 500
                    }
                },
                Project = new Project()
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
                        Name = "DeltaGamma",
                        Id = "DG",
                        Image = "DG",
                        Description = "DG Team"
                    }
                },
                Hours = 5
            };

            var controller = new TasksController();
            var response = controller.Post(t);
            var result = (OkNegotiatedContentResult<DetailModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void PutTaskSuccess()
        {
            Task t = new Task()
            {
                Id = 2,
                Day = unit.Days.Get(1),
                Description = "Simple maintenance task for us",
                Hours = 1,
                Project = unit.Projects.Get(1)
            };

            var controller = new TasksController();
            var response = controller.Put(t, 2);
            var result = (OkNegotiatedContentResult<DetailModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
