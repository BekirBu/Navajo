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
    public class DaysController : BaseController
    {
        public IHttpActionResult Get(int id, int year = 0, int month = 0)
        {
            if (year == 0) year = DateTime.Today.Year;
            if (month == 0) month = DateTime.Today.Month;
            var emp = TimeUnit.Employees.Get(id);
            //CalendarModel calendar = new CalendarModel(new BaseModel { Id = emp.Id, Name = emp.FirstName + " " + emp.LastName}, year, month);
            CalendarModel calendar = new CalendarModel(emp.Id, year, month);
            var days = emp.Days.Where(x => x.Date.Month == month && x.Date.Year == year).ToList();
            int i;
            foreach (var day in days)
            {
                i = day.Date.Day - 1;
                calendar.Days[i].Id = day.Id;
                calendar.Days[i].Type = (int)day.Type;
                calendar.Days[i].Hours = day.Hours;
                calendar.Days[i].Details = day.Tasks.Select(x => TimeFactory.Create(x)).ToArray();
                //calendar.Days[i].Employee.Id = day.Employee.Id;
                //calendar.Days[i].Employee.Name = day.Employee.FirstName;
                calendar.Days[i].EmployeeId = day.Employee.Id;
                calendar.Days[i].EmployeeName = day.Employee.FirstName;
            }
            return Ok(calendar);
        }

        public IHttpActionResult Post([FromBody] DayModel model)
        {
            try
            {
                Day day = new Day
                {
                    Id = model.Id,
                    Date = model.Date,
                    Type = (DayType)model.Type,
                    Hours = model.Hours,
                    Employee = TimeUnit.Employees.Get(model.EmployeeId)
                    //Employee = TimeUnit.Employees.Get(model.EmployeeId)
                };
                if (day.Id == 0)
                    TimeUnit.Days.Insert(day);
                else
                    TimeUnit.Days.Update(day, day.Id);
                TimeUnit.Save();

                foreach (DetailModel task in model.Details)
                {
                        Task detail = new Task
                        {
                            Id = task.Id,
                            Day = TimeUnit.Days.Get(day.Id),
                            Description = task.Description,
                            Hours = task.Hours,
                            Project = TimeUnit.Projects.Get(task.Project.Id)
                        };
                        if (detail.Id == 0)
                            TimeUnit.Tasks.Insert(detail);
                        else
                            TimeUnit.Tasks.Update(detail, detail.Id);
                    
                }
                TimeUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public IHttpActionResult Get(int id, int year = 0, int month = 0)
        //{
        //    if (year == 0) year = DateTime.Today.Year;
        //    if (month == 0) month = DateTime.Today.Month;
        //    Employee emp = TimeUnit.Employees.Get(id);
        //    CalendarModel calendar = new CalendarModel(new BaseModel { Id = emp.Id, Name = emp.FirstName + ' ' + emp.LastName}, year, month);

        //    var days = emp.Days.Where(x => x.Date.Month == month && x.Date.Year == year).ToList();
        //    int i;
        //    foreach (var day in days)
        //    {
        //        i = day.Date.Day - 1;
        //        calendar.Days[i].Id = day.Id;
        //        calendar.Days[i].Type = (int)day.Type;
        //        calendar.Days[i].Hours = day.Hours;
        //        calendar.Days[i].Details = day.Tasks.Select(x => TimeFactory.Create(x)).ToArray();
        //    }
        //    return Ok(calendar);
        //}

        //public IHttpActionResult Post([FromBody] DayModel model)
        //{
        //    try
        //    {
        //        Day day = new Day
        //        {
        //            Id = model.Id,
        //            Date = model.Date,
        //            Type = (DayType)model.Type,
        //            Hours = model.Hours,
        //            Employee = TimeUnit.Employees.Get(model.Employee.Id)
        //        };
        //        if (day.Id == 0)
        //            TimeUnit.Days.Insert(day);
        //        else
        //            TimeUnit.Days.Update(day, day.Id);
        //        TimeUnit.Save();

        //        foreach (DetailModel task in model.Details)
        //        {

        //                Task detail = new Task
        //                {
        //                    Id = task.Id,
        //                    Day = TimeUnit.Days.Get(day.Id),
        //                    Description = task.Description,
        //                    Hours = task.Hours,
        //                    Project = TimeUnit.Projects.Get(task.Project.Id)
        //                };
        //                if (detail.Id == 0)
        //                    TimeUnit.Tasks.Insert(detail);
        //                else
        //                    TimeUnit.Tasks.Update(detail, detail.Id);
        //            }

        //        TimeUnit.Save();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //public IHttpActionResult Delete(int id)
        //{
        //    try
        //    {
        //        Day day = TimeUnit.Days.Get(id);
        //        if (day == null)
        //        {
        //            Utility.Log($"Delete day failed. Wrong id.", "ERROR");
        //            return NotFound();
        //        }

        //        //TasksController assignments = new TasksController();
        //        //foreach (var eng in TimeUnit.Tasks.Get().Where(x => x.Day.Id == id))
        //        //{
        //        //    assignments.Delete(eng.Id);
        //        //}

        //        TimeUnit.Days.Delete(day);
        //        TimeUnit.Save();
        //        Utility.Log($"Deleted day with id " + id + ".", "INFO");
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.Log($"Delete day failed.", "ERROR");
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
