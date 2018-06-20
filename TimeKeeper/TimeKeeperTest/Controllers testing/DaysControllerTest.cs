//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Web;
//using System.Web.Http.Results;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using TimeKeeper.API.Controllers;
//using TimeKeeper.API.Models;
//using TimeKeeper.DAL;
//using TimeKeeper.DAL.Repositories;

//namespace TimeKeeperTest.Controllers_testing
//{
//    [TestClass]
//    public class DaysControllerTest
//    {
//        UnitOfWork unit = new UnitOfWork();

//        [TestInitialize]
//        public void Initialize()
//        {
//            HttpContext.Current = new HttpContext(
//                new HttpRequest("", "http://tempuri.org", ""),
//                new HttpResponse(new StringWriter()));
//        }

//        [TestMethod]
//        public void GetAllDaysSuccess()
//        {
//            var controller = new DaysController();

//            var response = controller.Get();
//            var result = (OkNegotiatedContentResult<List<DayModel>>)response;
//            Assert.IsNotNull(result);
//            Assert.IsNotNull(result.Content);
//        }

//        [TestMethod]
//        public void GetDaySuccess()
//        {
//            var controller = new DaysController();
//            var response = controller.GetById(1);
//            var result = (OkNegotiatedContentResult<DayModel>)response;

//            Assert.IsNotNull(result);
//            Assert.IsNotNull(result.Content);
//        }

//        [TestMethod]
//        public void DeleteDaySuccess()
//        {
//            Day d = new Day()
//            {
//                Hours = 8,
//                Comment = "This is a working day",
//                Type = DayType.WorkingDay,
//                Date = DateTime.Now,
//                Employee = null
//            };
            
//            unit.Days.Insert(d);
//            unit.Save();

//            var controller = new DaysController();
//            var response = controller.Delete(d.Id);
//            var result = (OkResult)response;

//            Assert.IsNotNull(result);
//        }

//        [TestMethod]
//        public void PostDaySuccess()
//        {
//            Day d = new Day()
//            {
//                Hours = 8,
//                Comment = "This is a working day",
//                Type = DayType.WorkingDay,
//                Date = DateTime.Now,
//                Employee = new Employee()
//                {
//                    FirstName = "Anela",
//                    LastName = "Hamic",
//                    Email = "mail@gmail.com",
//                    BirthDate = DateTime.Now.AddYears(-21),
//                    StatusEmployee = StatusEmployee.Active,
//                    BeginDate = DateTime.Now.AddYears(-1),
//                    Salary = 500
//                },
//                Tasks = new List<Task>()
//                {
//                    new Task()
//                    {
//                        Description = "Simple maintenance task",
//                        Hours = 2,
//                    }
//                }
//        };

//            var controller = new DaysController();
//            var response = controller.Post(d);
//            var result = (OkNegotiatedContentResult<DayModel>)response;

//            Assert.IsNotNull(result);
//            Assert.IsNotNull(result.Content);
//        }

//        [TestMethod]
//        public void PutDaySuccess()
//        {
//            Day d = new Day()
//            {
//                Id = 1,
//                Hours = 4,
//                Comment = "Used half day off",
//                Type = DayType.WorkingDay,
//                Date = DateTime.Now,
//                Employee = unit.Employees.Get(5)
//            };

//            var controller = new DaysController();
//            var response = controller.Put(d, 1);
//            var result = (OkNegotiatedContentResult<DayModel>)response;

//            Assert.IsNotNull(result);
//            Assert.IsNotNull(result.Content);
//        }
//    }
//}
