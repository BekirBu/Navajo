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
    public class ProjectsController : BaseController
    {
        [TimeAuth("Administrator")]
        public IHttpActionResult Get(int page = 0, int pageSize = 10,
                                     int sort = 0, string filter = "")
        {
            List<ProjectModel> list = new List<ProjectModel>();
            var query = TimeUnit.Projects.Get();

            Paging paging = new Paging();
            Filtering filtering = new Filtering();
            Sorting sorting = new Sorting();

            query = filtering.ProjectFiltering(query, filter);
            query = sorting.ProjectSorting(query, sort);
            list = paging.ProjectPaging(query, page, pageSize);

            int itemCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            Header h = new Header(totalPages, pageSize, page, sort);
            HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(h));

            Utility.Log($"Get data for projects.", "INFO");
            return Ok(list);
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult GetAll(string all)
        {
            var list = TimeUnit.Projects.Get().ToList().Select(x => TimeFactory.Create(x)).ToList();
            return Ok(list);
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult GetById(int id)
        {
            Project project = TimeUnit.Projects.Get(id);
            if (project == null)
            {
                Utility.Log($"Get data for project with id = " + id + " failed. Wrong id.", "ERROR");
                return NotFound();
            }
            else
            {
                Utility.Log($"Get data for project with id = " + id + ".", "INFO");
                return Ok(TimeFactory.Create(project));
            }
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Post([FromBody] Project project)
        {
            var errors = project.Validate();

            if (errors.Count > 0)
            {
                string combindedString = string.Join(" ", errors.ToArray());
                return BadRequest(combindedString);
            }

            try
            {
                TimeUnit.Projects.Insert(project);
                TimeUnit.Save();
                Utility.Log($"Inserted new project.", "INFO");
                return Ok(TimeFactory.Create(project));
            }
            catch (Exception ex)
            {
                Utility.Log($"Insert project failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Put([FromBody] Project project, int id)
        {
            try
            {
                if (TimeUnit.Projects.Get(id) == null)
                {
                    Utility.Log($"Update project failed. Wrong id.", "ERROR");
                    return NotFound();
                }

                var errors = project.Validate();

                if (errors.Count > 0)
                {
                    string combindedString = string.Join(" ", errors.ToArray());
                    return BadRequest(combindedString);
                }

                Project oldProject = TimeUnit.Projects.Get(id);
                if (oldProject == null) return NotFound();
                project = FillNewProjectWithOldData(project, oldProject);

                TimeUnit.Projects.Update(project, id);
                TimeUnit.Save();
                Utility.Log($"Updated project with id " + id + ".", "INFO");
                return Ok(TimeFactory.Create(project));
            }
            catch (Exception ex)
            {
                Utility.Log($"Update project failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        private Project FillNewProjectWithOldData(Project newProject, Project oldProject)
        {
            newProject.Id = oldProject.Id;
            newProject.CreatedBy = oldProject.CreatedBy;
            newProject.CreatedOn = oldProject.CreatedOn;

            if (newProject.Monogram == null && oldProject.Monogram != null)
                newProject.Monogram = oldProject.Monogram;

            if (newProject.Name == null && oldProject.Name != null)
                newProject.Name = oldProject.Name;

            if (newProject.BeginDate == null && oldProject.BeginDate != null)
                newProject.BeginDate = oldProject.BeginDate;

            if (newProject.Customer == null && oldProject.Customer != null)
                newProject.Customer = TimeUnit.Customer.Get(oldProject.Customer.Id);

            if (newProject.Description == null && oldProject.Description != null)
                newProject.Description = oldProject.Description;

            if (newProject.EndDate == null && oldProject.EndDate != null)
                newProject.EndDate = oldProject.EndDate;

            if (newProject.Team == null && oldProject.Team != null)
                newProject.Team = TimeUnit.Teams.Get(oldProject.Team.Id);

            if (newProject.TeamId == null)
                newProject.TeamId = newProject.Team.Id;

            if (newProject.CustomerId == 0)
                newProject.CustomerId = newProject.Customer.Id;

            if (newProject.Amount == null && oldProject.Amount != null)
                newProject.Amount = oldProject.Amount;
            return newProject;
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Project project = TimeUnit.Projects.Get(id);

                if (project == null)
                {
                    Utility.Log($"Delete project failed. Wrong id.", "ERROR");
                    return NotFound();
                }
                TimeUnit.Projects.Delete(project);
                TimeUnit.Save();
                Utility.Log($"Deleted project with id " + id + ".", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                Utility.Log($"Delete project failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}
