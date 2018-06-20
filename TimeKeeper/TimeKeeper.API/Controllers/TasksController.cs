using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TimeKeeper.API.Helper.Validation;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    public class TasksController : BaseController
    {

        public IHttpActionResult Get(int page = 0, int pageSize = 10,
                                     int sort = 0, string filter = "")
        {
            List<DetailModel> list = new List<DetailModel>();
            var query = TimeUnit.Tasks.Get();

            Paging paging = new Paging();
            Filtering filtering = new Filtering();
            Sorting sorting = new Sorting();

            query = filtering.TaskFiltering(query, filter);
            query = sorting.TaskSorting(query, sort);
            list = paging.TaskPaging(query, page, pageSize);

            //Header h = new Header(page, sort);
            //HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(h));

            Utility.Log($"Get data for tasks.", "INFO");
            return Ok(list);
        }

        public IHttpActionResult GetById(int id)
        {
            Task task = TimeUnit.Tasks.Get(id);
            if (task == null)
            {
                Utility.Log($"Get data for task with id = " + id + " failed. Wrong id.", "ERROR");
                return NotFound();
            }
            else
            {
                Utility.Log($"Get data for task with id = " + id + ".", "INFO");
                return Ok(TimeFactory.Create(task));
            }
        }

        public IHttpActionResult Post([FromBody] Task task)
        {
            var errors = task.Validate();

            if (errors.Count > 0)
            {
                string combindedString = string.Join(" ", errors.ToArray());
                return BadRequest(combindedString);
            }

            try
            {
                task.Day = TimeUnit.Days.Get(task.Day.Id);
                task.Project = TimeUnit.Projects.Get(task.Project.Id);

                TimeUnit.Tasks.Insert(task);
                TimeUnit.Save();
                Utility.Log($"Inserted new task.", "INFO");
                return Ok(TimeFactory.Create(task));
            }
            catch (Exception ex)
            {
                Utility.Log($"Insert task failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Task task, int id)
        {
            try
            {
                if (TimeUnit.Tasks.Get(id) == null)
                {
                    Utility.Log($"Update task failed. Wrong id.", "ERROR");
                    return NotFound();
                }

                var errors = task.Validate();

                if (errors.Count > 0)
                {
                    string combindedString = string.Join(" ", errors.ToArray());
                    return BadRequest(combindedString);
                }

                TimeUnit.Tasks.Update(task, id);
                TimeUnit.Save();
                Utility.Log($"Updated task with id " + id + ".", "INFO");
                return Ok(TimeFactory.Create(task));
            }
            catch (Exception ex)
            {
                Utility.Log($"Update task failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                Task task = TimeUnit.Tasks.Get(id);
                if (task == null)
                {
                    Utility.Log($"Delete task failed. Wrong id.", "ERROR");
                    return NotFound();
                }
                TimeUnit.Tasks.Delete(task);
                TimeUnit.Save();
                Utility.Log($"Deleted task with id " + id + ".", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                Utility.Log($"Delete task failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }


    }
}