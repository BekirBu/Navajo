using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.API.Helper.Reports;

namespace TimeKeeper.API.Controllers
{
    public class ReportsController : BaseController
    {
        //Invoices
        [Route("api/invoices/{year}/{month}")]
        public IHttpActionResult GetInvoices(int year, int month)
        {
            return Ok(TimeUnit.GetInvoiceReport(year, month, TimeFactory));
        }


        //Personal report - user dashboard
        [Route("api/reports/personal/{employeeId}/{year}/{month}")]
        public IHttpActionResult GetPersonalReport(int employeeId, int year, int month)
        {
            if (TimeUnit.Employees.Get(employeeId) == null)
            {
                return NotFound();
            }

            return Ok(TimeUnit.GetPersonalReport(employeeId, year, month, TimeFactory));
        }

        //Company report - admin dashboard
        [Route("api/reports/company/{year}/{month}")]
        public IHttpActionResult GetCompanyReport(int year, int month)
        {
            return Ok(TimeUnit.GetCompanyReport(year, month, TimeFactory));
        }

        //Team report - team leader dashboard
        [Route("api/reports/team/{teamId}/{year}/{month}")]
        public IHttpActionResult GetTeamReport(string teamId, int year, int month)
        {
            if (TimeUnit.Teams.Get(teamId) == null) return NotFound();
            return Ok(TimeUnit.GetTeamReport(teamId, year, month, TimeFactory));
        }

        //Monthly report
        [Route("api/reports/monthly/{year}/{month}")]
        public IHttpActionResult GetMonthlyReport(int year, int month)
        {
            return Ok(TimeUnit.GetMonthlyReport(year, month, TimeFactory));
        }

        //Annual report
        [Route("api/reports/annual/{year}")]
        public IHttpActionResult GetAnnualOverview(int year)
        {
            return Ok(TimeUnit.GetAnnualOverview(year, TimeFactory));
        }

        //Project History report
        [Route("api/reports/history/{id}")]
        public IHttpActionResult GetProjectHistory(int id)
        {
            return Ok(TimeUnit.GetProjectHistory(id, TimeFactory));
        }
    }
}
