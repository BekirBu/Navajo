using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Helper.Validation;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Repositories;


namespace TimeKeeper.API.Controllers
{
    public class TeamsController : BaseController
    {
        //[TimeAuth("Administrator,User")]
        public IHttpActionResult Get(int page = 0, int pageSize = 10,
                                     int sort = 0, string filter = "")
        {
            List<TeamModel> list = new List<TeamModel>();
            var query = TimeUnit.Teams.Get();

            Paging paging = new Paging();
            Filtering filtering = new Filtering();
            Sorting sorting = new Sorting();

            query = filtering.TeamFiltering(query, filter);
            query = sorting.TeamSorting(query, sort);
            list = paging.TeamPaging(query, page, pageSize);

            //Header h = new Header(page, sort);
            //HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(h));

            Utility.Log($"Get data for teams.", "INFO");

            return Ok(list);
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult GetById(string id)
        {
            Team team = TimeUnit.Teams.Get(id);
            if (team == null)
            {
                Utility.Log($"Get data for team with id" + id + " failed. Wrong id.", "ERROR");
                return NotFound();
            }
            else
            {
                Utility.Log($"Get data for team with id" + id + ".", "INFO");
                return Ok(TimeFactory.Create(team));
            }
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Post([FromBody] Team team)
        {
            var errors = team.Validate();

            if (errors.Count > 0)
            {
                string combindedString = string.Join(" ", errors.ToArray());
                return BadRequest(combindedString);
            }

            try
            {
                team.Id = team.Name.Substring(0,3).ToUpper();
                TimeUnit.Teams.Insert(team);
                TimeUnit.Save();
                Utility.Log($"Insert new team.", "INFO");
                return Ok(TimeFactory.Create(team));
            }
            catch (Exception ex)
            {
                Utility.Log($"Insert team failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Put([FromBody] Team team, string id)
        {
            try
            {
                if (TimeUnit.Teams.Get(id) == null)
                {
                    Utility.Log($"Update with id " + id + " failed. Wrong id.", "ERROR");
                    return NotFound();
                }

                var errors = team.Validate();

                if (errors.Count > 0)
                {
                    string combindedString = string.Join(" ", errors.ToArray());
                    return BadRequest(combindedString);
                }

                TimeUnit.Teams.Update(team, id);
                TimeUnit.Save();
                Utility.Log($"Updated team with id " + id + ".", "INFO");
                return Ok(TimeFactory.Create(team));
            }
            catch (Exception ex)
            {
                Utility.Log($"Update team failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                Team team = TimeUnit.Teams.Get(id);
                if (team == null)
                {
                    Utility.Log($"Delete team failed. Wrong id.", "ERROR");
                    return NotFound();
                }
                TimeUnit.Teams.Delete(team);
                Utility.Log($"Delete team with id " + id + ".", "INFO");
                TimeUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                Utility.Log($"Delete team failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}