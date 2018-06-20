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
    public class EmployeesControllerTest
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
        public void GetAllEmployeesSuccess()
        {
            var controller = new EmployeesController();
            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<EmployeeModel>>)response;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void GetEmployeeSuccess()
        {
            var controller = new EmployeesController();
            var response = controller.GetById(1);
            var result = (OkNegotiatedContentResult<EmployeeModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DeleteEmployeeSuccess()
        {
            Employee e = new Employee()
            {
                FirstName = "Minela",
                LastName = "Hamic",
                Email = "mail@gmail.com",
                BirthDate = DateTime.Now.AddYears(-21),
                StatusEmployee = StatusEmployee.Active,
                BeginDate = DateTime.Now.AddYears(-1),
                Salary = 2500,
                Position = unit.Roles.Get("QA")
            };

            unit.Employees.Insert(e);
            unit.Save();

            var controller = new EmployeesController();
            var response = controller.Delete(e.Id);
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostEmployeeSuccess()
        {
            Employee e = new Employee()
            {
                FirstName = "Anela",
                LastName = "Hamic",
                Email = "mail@gmail.com",
                BirthDate = DateTime.Now.AddYears(-21),
                StatusEmployee = StatusEmployee.Active,
                BeginDate = DateTime.Now.AddYears(-1),
                EndDate = DateTime.Now,
                Phone = "+87765643",
                Salary = 800,
                Position = new Role()
                {
                    Id = "SD11",
                    Hrate = 8,
                    Mrate = 1000,
                    Type = RoleType.TeamRole
                }
            };

            var controller = new EmployeesController();
            var response = controller.Post(e);
            var result = (OkNegotiatedContentResult<EmployeeModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void PutEmployeeSuccess()
        {
            Employee e = new Employee()
            {
                Id = 2,
                FirstName = "Minela",
                LastName = "Hodzic",
                Email = "mail@gmail.com",
                BirthDate = DateTime.Now.AddYears(-21),
                StatusEmployee = StatusEmployee.Active,
                BeginDate = DateTime.Now.AddYears(-1),
                Salary = 2500,
                Phone = "+87765643",
                Position = new Role()
                {
                    Id = "SD11",
                    Hrate = 8,
                    Mrate = 1000,
                    Type = RoleType.TeamRole
                }
            };

            var controller = new EmployeesController();
            var response = controller.Put(e, 2);
            var result = (OkNegotiatedContentResult<EmployeeModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
