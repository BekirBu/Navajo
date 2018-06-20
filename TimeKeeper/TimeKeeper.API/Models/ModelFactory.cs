using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models.ReportsModel;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Models
{
    public class ModelFactory
    {
        public CustomerModel Create(Customer c)
        {
            return new CustomerModel()
            {
                Id = c.Id,
                Name = c.Name,
                Image = c.Image,
                Monogram = c.Monogram,
                Contact = c.Contact,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                StatusCustomer = Convert.ToInt32(c.StatusCustomer),
                //Projects = c.Projects.Select(p => Create(p)).ToList(),
            };
        }

        //public DayModel Create(BaseModel e, int year, int month)
        //{
        //    return new DayModel(e, year, month)
        //    {
        //        Employee = new BaseModel { Id = e.Id, Name = e.Name},

        //        Month = month,
        //        Year = year
        //    };
        //}

        //public OneDayModel Create(BaseModel e)
        //{
        //    return new OneDayModel(e)
        //    {


        //    };
        //}

        public EmployeeModel Create(Employee e)
        {
            return new EmployeeModel()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Name = e.FirstName + ' ' + e.LastName,
                //Image = e.Image,
                Image = "data:image/png;base64, " + e.ConvertToBase64(),
                Email = e.Email,
                Phone = e.Phone,
                BirthDate = e.BirthDate,
                BeginDate = e.BeginDate,
                EndDate = e.EndDate,
                Salary = e.Salary,
                Position = Create(e.Position),
                RoleId = e.RoleId,
                StatusEmployee = Convert.ToInt32(e.StatusEmployee),
                Projects = e.Engagement.Select(x => x.Team).SelectMany(y => y.Projects).Select(p => new BaseModel()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList()
                //Engagements = e.Engagement.Select(en => Create(en)).ToList(),
                //Days = e.Days.Select(d => Create(d)).ToList(),
            };
        }

        public EngagementModel Create(Engagement e)
        {
            return new EngagementModel()
            {
                Id = e.Id,
                Team = (e.Team != null) ? e.Team.Name : "/",
                TeamId = (e.Team != null) ? e.Team.Id : "/",
                Role = e.Role.Name,
                Employee = (e.Employee != null) ? e.Employee.FirstName + " " + e.Employee.LastName : "/",
                EmployeeId = (e.Employee != null) ? e.Employee.Id : 0,
                Hours = e.Hours
            };
        }

        public ProjectModel Create(Project p)
        {
            return new ProjectModel()
            {
                Id = p.Id,
                Name = p.Name,
                Monogram = p.Monogram,
                Description = p.Description,
                BeginDate = p.BeginDate,
                EndDate = p.EndDate,
                //StatusProject = Convert.ToInt32(p.StatusProject),
                StatusProject = p.StatusProject.ToString(),
                Pricing = p.Pricing.ToString(),
                Amount = p.Amount,
                CustomerId = (p.Customer != null) ? p.CustomerId : 0,
                TeamId = (p.Team != null) ? p.TeamId : "/",
                Customer = (p.Customer != null) ? p.Customer.Name : "/",
                Team = (p.Team != null) ? p.Team.Name : "/"
                //Tasks = p.Tasks.Select(t => Create(t)).ToList()
            };
        }

        public RoleModel Create(Role r)
        {
            return new RoleModel()
            {
                Id = r.Id,
                Name = r.Name,
                Type = r.Type.ToString(),
                Hrate = r.Hrate,
                Mrate = r.Mrate,
                //Engagements = r.Engagements.Select(e => Create(e)).ToList(),
                //Employees = r.Employees.Select(emp => Create(emp)).ToList()
            };
        }

        public DetailModel Create(Task ta)
        {
            return new DetailModel()
            {

                Id = ta.Id,
                Description = ta.Description,
                Hours = ta.Hours,
                Project = new BaseModel { Id = ta.Project.Id, Name = ta.Project.Name },
                Deleted = ta.Deleted
            };
        }

        public TeamModel Create(Team t)
        {
            return new TeamModel()
            {
                Id = t.Id,
                Name = t.Name,
                Image = t.Image,
                Description = t.Description,
                Members = t.Engagements.Where(x => x.Employee != null).Select(e => Create(e)).ToList(),
                //Projects = t.Projects.Select(p => Create(p)).ToList()
            };
        }

        public UserModel CreateUser(Employee emp, string provider)
        {
            return new UserModel()
            {
                Id = emp.Id,
                Name = emp.FirstName + " " + emp.LastName,
                Role = emp.Position.Name,
                Teams = emp.Engagement.Select(x => x.Team.Name).ToList(),
                Provider = provider,
                TeamLeadTo = emp.Engagement.Where(x => x.Role.Id.Contains("TL")).Select(x => x.Team.Id).ToList()
            };
        }

        public MonthlyReportTasks CreateMonthlyReportTask(Task t)
        {
            return new MonthlyReportTasks
            {
                Hours = t.Hours,
                Description = t.Description
            };
        }

        public MonthlyReportProjects CreateMonthlyReportProject(Project p, int Id, int month, int year)
        {
            return new MonthlyReportProjects
            {
                Amount = p.Amount,
                BeginDate = p.BeginDate,
                Description = p.Description,
                EndDate = p.EndDate,
                Monogram = p.Monogram,
                Name = p.Name,
                Pricing = p.Pricing.ToString(),
                Tasks = p.Tasks.Where(x => x.Day.Employee.Id == Id && x.Day.Date.Month == month && x.Day.Date.Year == year)
                                .Select(x => CreateMonthlyReportTask(x))
                                .ToList(),
                Hours = p.Tasks.Where(x => x.Day.Employee.Id == Id && x.Day.Date.Month == month && x.Day.Date.Year == year)
                                .Select(x => x.Hours)
                                .Sum()
            };
        }

        public AnnualReportEmployees CreateAnnualReportEmoloyees(Employee e)
        {
            return new AnnualReportEmployees
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Image = e.Image,
                Phone = e.Phone
            };
        }

        //public HistoryReportEmployees CreateHistoryReportEmoloyees(Project p, int year)
        //{
        //    return new HistoryReportEmployees
        //    {
        //        FirstName = p.Team.Engagements.Select(e => e.Employee.FirstName).ToString(),
        //        LastName = p.Team.Engagements.Select(e => e.Employee.LastName).ToString(),
        //        Email = p.Team.Engagements.Select(e => e.Employee.Email).ToString(),
        //        Image = p.Team.Engagements.Select(e => e.Employee.Image).ToString(),
        //        Phone = p.Team.Engagements.Select(e => e.Employee.Phone).ToString(),
        //        //Hours = p.Tasks.Where(y => y.Day.Date.Year == year && y.Project.Id == id)

        //        //Days.Where(y => y.Date.Year == year)
        //        //              .GroupBy(g => g.Date.Month)
        //        //              .Select(d => d.Sum(h => h.Hours))
        //        //              .ToList()
        //    };
        //}
    }
}