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
using TimeKeeper.DAL.Repositories;

namespace TimeKeeper.API.Controllers
{
    public class RolesController : BaseController
    {
        public IHttpActionResult Get(int page = 0, int pageSize = 10,
                                     int sort = 0, string filter = "")
        {
            List<RoleModel> list = new List<RoleModel>();
            var query = TimeUnit.Roles.Get();

            Paging paging = new Paging();
            Filtering filtering = new Filtering();
            Sorting sorting = new Sorting();

            query = filtering.RoleFiltering(query, filter);
            query = sorting.RoleSorting(query, sort);
            list = paging.RolePaging(query, page, pageSize);

            //Header h = new Header(page, sort);
            //HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(h));

            Utility.Log($"Get data for roles.", "INFO");
            return Ok(list);
        }

        public IHttpActionResult GetById(string id)
        {
            Role role = TimeUnit.Roles.Get(id);
            if (role == null)
            {
                Utility.Log($"Get data for role with id = " + id + " failed. Wrong id.", "ERROR");
                return NotFound();
            }
            else
            {
                Utility.Log($"Get data for role with id = " + id + ".", "INFO");
                return Ok(TimeFactory.Create(role));
            }
        }

        public IHttpActionResult Post([FromBody] Role role)
        {
            var errors = role.Validate();

            if (errors.Count > 0)
            {
                string combindedString = string.Join(" ", errors.ToArray());
                return BadRequest(combindedString);
            }

            try
            {
                TimeUnit.Roles.Insert(role);
                TimeUnit.Save();
                Utility.Log($"Inserted new role.", "INFO");
                return Ok(TimeFactory.Create(role));
            }
            catch (Exception ex)
            {
                Utility.Log($"Insert role failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Role role, string id)
        {
            try
            {
                if (TimeUnit.Roles.Get(id) == null)
                {
                    Utility.Log($"Update role failed. Wrong id.", "ERROR");
                    return NotFound();
                }

                var errors = role.Validate();

                if (errors.Count > 0)
                {
                    string combindedString = string.Join(" ", errors.ToArray());
                    return BadRequest(combindedString);
                }

                TimeUnit.Roles.Update(role, id);
                TimeUnit.Save();
                Utility.Log($"Updated role with id " + id + ".", "INFO");
                return Ok(TimeFactory.Create(role));
            }
            catch (Exception ex)
            {
                Utility.Log($"Update role failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(string id)
        {
            try
            {
                Role role = TimeUnit.Roles.Get(id);
                if (role == null)
                {
                    Utility.Log($"Delete role failed. Wrong id.", "ERROR");
                    return NotFound();
                }
                TimeUnit.Roles.Delete(role);
                TimeUnit.Save();
                Utility.Log($"Deleted role with id " + id + ".", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                Utility.Log($"Delete role failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}
