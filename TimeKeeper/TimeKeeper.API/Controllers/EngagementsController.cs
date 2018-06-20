using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Helper.Validation;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    public class EngagementsController : BaseController
    {
        public IHttpActionResult Get(int page = 0, int pageSize = 10,
                                     int sort = 0, string filter = "")
        {
            List<EngagementModel> list = new List<EngagementModel>();
            var query = TimeUnit.Engagements.Get();

            Paging paging = new Paging();
            Filtering filtering = new Filtering();
            Sorting sorting = new Sorting();

            query = filtering.EngagementFiltering(query, filter);
            query = sorting.EngagementSorting(query, sort);
            list = paging.EngagementPaging(query, page, pageSize);

            //Header h = new Header(page, sort);
            //HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(h));

            Utility.Log($"Get data for engagements.", "INFO");
            return Ok(list);
        }

        public IHttpActionResult GetById(int id)
        {
            Engagement engagement = TimeUnit.Engagements.Get(id);
            if (engagement == null)
            {
                Utility.Log($"Get data for engagement with id = " + id + " failed. Wrong id.", "ERROR");
                return NotFound();
            }
            else
            {
                Utility.Log($"Get data for engagement with id = " + id + ".", "INFO");
                return Ok(TimeFactory.Create(engagement));
            }
        }

        public IHttpActionResult Post([FromBody] Engagement engagement)
        {
            var errors = engagement.Validate();

            if (errors.Count > 0)
            {
                string combindedString = string.Join(" ", errors.ToArray());
                return BadRequest(combindedString);
            }

            engagement.Team = TimeUnit.Teams.Get(engagement.Team.Id);
            engagement.Employee = TimeUnit.Employees.Get(engagement.Employee.Id);
            engagement.Role = TimeUnit.Roles.Get(engagement.Role.Id);

            try
            {
                TimeUnit.Engagements.Insert(engagement);
                TimeUnit.Save();
                Utility.Log($"Inserted new engagement.", "INFO");
                return Ok(TimeFactory.Create(engagement));
            }
            catch (Exception ex)
            {
                Utility.Log($"Insert engagement failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Engagement engagement, int id)
        {
            try
            {
                if (TimeUnit.Engagements.Get(id) == null)
                {
                    Utility.Log($"Update engagement failed. Wrong id.", "ERROR");
                    return NotFound();
                }

                var errors = engagement.Validate();

                if (errors.Count > 0)
                {
                    string combindedString = string.Join(" ", errors.ToArray());
                    return BadRequest(combindedString);
                }

                TimeUnit.Engagements.Update(engagement, id);
                TimeUnit.Save();
                Utility.Log($"Updated engagement with id " + id + ".", "INFO");
                return Ok(TimeFactory.Create(engagement));
            }
            catch (Exception ex)
            {
                Utility.Log($"Update engagement failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                Engagement engagement = TimeUnit.Engagements.Get(id);
                if (engagement == null)
                {
                    Utility.Log($"Delete engagement failed. Wrong id.", "ERROR");
                    return NotFound();
                }
                TimeUnit.Engagements.Delete(engagement);
                TimeUnit.Save();
                Utility.Log($"Deleted engagement with id " + id + ".", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                Utility.Log($"Delete engagement failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}
