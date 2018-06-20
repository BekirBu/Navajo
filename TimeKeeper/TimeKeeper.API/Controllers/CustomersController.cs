using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Helper.Validation;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    public class CustomersController : BaseController
    {
        [TimeAuth("Administrator")]
        public IHttpActionResult Get(int page = 0, int pageSize = 10,
                                     int sort = 0, string filter = "")
        {
            List<CustomerModel> list = new List<CustomerModel>();
            var query = TimeUnit.Customer.Get();

            Paging paging = new Paging();
            Filtering filtering = new Filtering();
            Sorting sorting = new Sorting();
            
            query = filtering.CustomerFiltering(query, filter);
            query = sorting.CustomerSorting(query, sort);
            list = paging.CustomerPaging(query, page, pageSize);

            //Header h = new Header(page, sort);
            //HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(h));

            Utility.Log($"Get data for customers.", "INFO");
            return Ok(list);
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult GetById(int id)
        {
            Customer customer = TimeUnit.Customer.Get(id);
            if (customer == null)
            {
                Utility.Log($"Get data for customer with id = " + id + " failed. Wrong id.", "ERROR");
                return NotFound();
            }
            else
            {
                Utility.Log($"Get data for customer with id = " + id + ".", "INFO");
                return Ok(TimeFactory.Create(customer));
            }
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Post([FromBody] Customer customer)
        {
            var errors = customer.Validate();

            if (errors.Count > 0)
            {
                string combindedString = string.Join(" ", errors.ToArray());
                return BadRequest(combindedString);
            }

            try
            {
                TimeUnit.Customer.Insert(customer);
                TimeUnit.Save();
                Utility.Log($"Inserted new customer.", "INFO");
                return Ok(TimeFactory.Create(customer));
            }
            catch (Exception ex)
            {
                Utility.Log($"Insert customer failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Put([FromBody] Customer customer, int id)
        {
            try
            {
                if (TimeUnit.Customer.Get(id) == null)
                {
                    Utility.Log($"Update customer failed. Wrong id.", "ERROR");
                    return NotFound();
                }

                var errors = customer.Validate();

                if (errors.Count > 0)
                {
                    string combindedString = string.Join(" ", errors.ToArray());
                    return BadRequest(combindedString);
                }

                //customer.Address = TimeUnit.Customer.Get().Where(x => x.Id == id).Select(y => y.Address).FirstOrDefault();

                TimeUnit.Customer.Update(customer, id);
                TimeUnit.Save();
                Utility.Log($"Updated customer with id " + id + ".", "INFO");
                return Ok(TimeFactory.Create(customer));
            }
            catch (Exception ex)
            {
                Utility.Log($"Update customer failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Customer customer = TimeUnit.Customer.Get(id);
                if (customer == null)
                {
                    Utility.Log($"Delete customer failed. Wrong id.", "ERROR");
                    return NotFound();
                }
                TimeUnit.Customer.Delete(customer);
                TimeUnit.Save();
                Utility.Log($"Deleted customer with id " + id + ".", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                Utility.Log($"Delete customer failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}
