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

namespace TimeKeeper.API.Controllers
{
    public class EmployeesController : BaseController
    {
        [TimeAuth("Administrator")]
        public IHttpActionResult Get(int page = 0, int pageSize = 10,
                                     int sort = 0, string filter = "")
        {
            List<EmployeeModel> list = new List<EmployeeModel>();
            var query = TimeUnit.Employees.Get();

            Filtering filtering = new Filtering();
            Sorting sorting = new Sorting();
            Paging paging = new Paging();

            query = filtering.EmployeeFiltering(query, filter);
            query = sorting.EmployeeSorting(query, sort);
            list = paging.EmployeePaging(query, page, pageSize);

            int itemCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            Header h = new Header(totalPages, pageSize, page, sort);
            HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(h));

            Utility.Log($"Get data for employees.", "INFO");
            return Ok(list);
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult GetAll(string all)
        {
            var list = TimeUnit.Employees.Get().ToList().Select(x => TimeFactory.Create(x)).ToList();
            return Ok(list);
        }

        //[TimeAuth("Administrator")]
        public IHttpActionResult GetById(int id)
        {
            Employee employee = TimeUnit.Employees.Get(id);
            if (employee == null)
            {
                Utility.Log($"Get data for employee with id = " + id + " failed.", "ERROR");
                return NotFound();
            }
            else
            {
                employee.Image = employee.ConvertToBase64();
                Utility.Log($"Get data for employee with id = " + id + ".", "INFO");
                return Ok(TimeFactory.Create(employee));
            }
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Post([FromBody] Employee employee)
        {
            var errors = employee.Validate();

            if (errors.Count > 0)
            {
                string combindedString = string.Join(" ", errors.ToArray());
                return BadRequest(combindedString);
            }

            employee.Position = TimeUnit.Roles.Get(x => x.Id == employee.Position.Id).FirstOrDefault();

            try
            {
                employee.Image = employee.ConvertAndSave();

                TimeUnit.Employees.Insert(employee);
                TimeUnit.Save();
                Utility.Log($"Inserted new employee.", "INFO");
                return Ok(TimeFactory.Create(employee));
            }
            catch (Exception ex)
            {
                Utility.Log($"Insert employee failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Put([FromBody] Employee employee, int id)
        {
            try
            {
                if (TimeUnit.Employees.Get(id) == null)
                {
                    Utility.Log($"Update employee failed. Wrong id.", "ERROR");
                    return NotFound();
                }

                var errors = employee.Validate();

                if (errors.Count > 0)
                {
                    string combindedString = string.Join(" ", errors.ToArray());
                    return BadRequest(combindedString);
                }

                Employee oldEmployee = TimeUnit.Employees.Get(id);
                if (oldEmployee == null) return NotFound();
                employee = FillEmployeeWithOldData(employee, oldEmployee);
                employee.Position = TimeUnit.Roles.Get(employee.Position.Id);

                //id = TimeUnit.Employees.Get().Where(x => x.Id == id).Select(x => x.Id).FirstOrDefault();
                if (TimeUnit.Employees.Get(id) == null) return NotFound();

                employee.Image = employee.ConvertAndSave();

                TimeUnit.Employees.Update(employee, id);
                TimeUnit.Save();
                Utility.Log($"Updated employee with id " + id + ".", "INFO");
                return Ok(TimeFactory.Create(employee));
            }
            catch (Exception ex)
            {
                Utility.Log($"Update employee failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }

        private Employee FillEmployeeWithOldData(Employee newEmployee, Employee oldEmployee)
        {
            newEmployee.Id = oldEmployee.Id;
            newEmployee.CreatedBy = oldEmployee.CreatedBy;
            newEmployee.CreatedOn = oldEmployee.CreatedOn;


            if (newEmployee.Days.Count == 0 && oldEmployee.Days.Count != 0)
                newEmployee.Days = oldEmployee.Days;

            if (newEmployee.Engagement.Count == 0 && oldEmployee.Engagement.Count != 0)
                newEmployee.Engagement = oldEmployee.Engagement;

            if (newEmployee.Position == null && oldEmployee.Position != null)
                newEmployee.Position = TimeUnit.Roles.Get(oldEmployee.Position.Id);

            if (newEmployee.Image == null && oldEmployee.Image != null)
                newEmployee.Image = oldEmployee.Image;

            if (newEmployee.FirstName == null && oldEmployee.FirstName != null)
                newEmployee.FirstName = oldEmployee.FirstName;

            if (newEmployee.LastName == null && oldEmployee.LastName != null)
                newEmployee.LastName = oldEmployee.LastName;

            if (newEmployee.Phone == null && oldEmployee.Phone != null)
                newEmployee.Phone = oldEmployee.Phone;

            if (newEmployee.Salary == null && oldEmployee.Salary != null)
                newEmployee.Salary = oldEmployee.Salary;

            if (newEmployee.StatusEmployee == null && oldEmployee.StatusEmployee != null)
                newEmployee.StatusEmployee = oldEmployee.StatusEmployee;

            if (newEmployee.Email == null && oldEmployee.Email != null)
                newEmployee.Email = oldEmployee.Email;

            if (newEmployee.BeginDate == null && oldEmployee.BeginDate != null)
                newEmployee.BeginDate = oldEmployee.BeginDate;

            if (newEmployee.BirthDate == null && oldEmployee.BirthDate != null)
                newEmployee.BirthDate = oldEmployee.BirthDate;

            if (newEmployee.EndDate == null && oldEmployee.EndDate != null)
                newEmployee.EndDate = oldEmployee.EndDate;

            if (newEmployee.Password == null && oldEmployee.Password != null)
                newEmployee.Password = oldEmployee.Password;

            return newEmployee;
        }

        [TimeAuth("Administrator")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Employee employee = TimeUnit.Employees.Get(id);
                if (employee == null)
                {
                    Utility.Log($"Delete employee failed. Wrong id.", "ERROR");
                    return NotFound();
                }

                //DaysController days = new DaysController();
                //foreach (var eng in TimeUnit.Days.Get().Where(x => x.Employee.Id == id))
                //{
                //    days.Delete(eng.Id);
                //}

                //EngagementsController members = new EngagementsController();
                //foreach (var eng in TimeUnit.Engagements.Get().Where(x => x.Employee.Id == id))
                //{
                //    members.Delete(eng.Id);
                //}

                TimeUnit.Employees.Delete(employee);
                TimeUnit.Save();
                Utility.Log($"Deleted employee with id " + id + ".", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                Utility.Log($"Delete employee failed.", "ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}
