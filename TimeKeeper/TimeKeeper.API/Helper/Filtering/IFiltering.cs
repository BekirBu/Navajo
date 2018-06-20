using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Helper
{
    interface IFiltering
    {
        IQueryable<Customer> CustomerFiltering(IQueryable<Customer> query, string filter);
        IQueryable<Day> DayFiltering(IQueryable<Day> query, string filter);
        IQueryable<Employee> EmployeeFiltering(IQueryable<Employee> query, string filter);
        IQueryable<Engagement> EngagementFiltering(IQueryable<Engagement> query, string filter);
        IQueryable<Project> ProjectFiltering(IQueryable<Project> query, string filter);
        IQueryable<Role> RoleFiltering(IQueryable<Role> query, string filter);
        IQueryable<DAL.Task> TaskFiltering(IQueryable<DAL.Task> query, string filter);
        IQueryable<Team> TeamFiltering(IQueryable<Team> query, string filter);
    }
}
