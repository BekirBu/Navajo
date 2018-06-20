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
    public class CustomersControllerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter()));
        }

        [TestMethod]
        public void GetAllCustomersSuccess()
        {
            var controller = new CustomersController();
            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<CustomerModel>>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void GetCustomerSuccess()
        {
            var controller = new CustomersController();
            var response = controller.GetById(1);
            var result = (OkNegotiatedContentResult<CustomerModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DeleteCustomerSuccess()
        {
            Address add = new Address
            {
                City = "Sarajevo",
                Name = "Bosmal",
                ZipCode = "71000"
            };

            Customer c = new Customer();

            c = new Customer()
            {
                Name = "Mistral Technologies new",
                Image = null,
                Monogram = null,
                Contact = "Sulejman Catibusic",
                Email = "info@mistral.ba",
                Phone = "+38761200333",
                StatusCustomer = StatusCustomer.Client,
                Address = add
            };

            UnitOfWork unit = new UnitOfWork();
            unit.Customer.Insert(c);
            unit.Save();

            var controller = new CustomersController();
            var response = controller.Delete(c.Id);
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostCustomerSuccess()
        {
            Address add = new Address
            {
                City = "Sarajevo",
                Name = "Bosmal",
                ZipCode = "71000"
            };

            Customer c = new Customer()
            {
                Name = "Maestral Solutions",
                Image = null,
                Monogram = null,
                Contact = "Sule Catibusic",
                Email = "info@mistral.ba",
                Phone = "+38761200333",
                StatusCustomer = StatusCustomer.Client,
                Address = add
            };

            var controller = new CustomersController();
            var response = controller.Post(c);
            var result = (OkNegotiatedContentResult<CustomerModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void PutCustomerSuccess()
        {
            Address add = new Address
            {
                City = "Sarajevo",
                Name = "Bosmal",
                ZipCode = "71000"
            };

            Customer c = new Customer()
            {
                Id = 1,
                Name = "New maestral Solutions",
                Image = null,
                Monogram = null,
                Contact = "Sule Catibusic",
                Email = "info@mistral.ba",
                Phone = "+38761200333",
                StatusCustomer = StatusCustomer.Client,
                Address = add
            };

            var controller = new CustomersController();
            var response = controller.Put(c, 1);
            var result = (OkNegotiatedContentResult<CustomerModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

    }
}
