using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TimeKeeper.API.Controllers
{
    public class ReportsController : BaseController
    {
        public IHttpActionResult Get(int month, string filter = "")
        {
            month = DateTime.Now.Month;
            //List<EmployeesProjectsModel> list = new List<EmployeesProjectsModel>();
            //var query = TimeUnit.Employees.Get().GroupBy(x => x.pr);

            //List<CustomerModel> list = new List<CustomerModel>();
            //var query = TimeUnit.Customer.Get();

            //Paging paging = new Paging();
            //Filtering filtering = new Filtering();
            //Sorting sorting = new Sorting();

            //query = filtering.CustomerFiltering(query, filter);
            //query = sorting.CustomerSorting(query, sort);
            //list = paging.CustomerPaging(query, page, pageSize);

            //Header h = new Header(page, sort);
            //HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(h));

            //Utility.Log($"Get data for customers.", "INFO");
            //return Ok(list);

            return Ok();
        }
    }
}
