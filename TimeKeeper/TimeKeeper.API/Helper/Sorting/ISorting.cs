using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Helper
{
    interface ISorting
    {
        IQueryable<Customer> CustomerSorting(IQueryable<Customer> query, int sort);
        IQueryable<Day> DaySorting(IQueryable<Day> query, int sort);
        IQueryable<Employee> EmployeeSorting(IQueryable<Employee> query, int sort);
        IQueryable<Engagement> EngagementSorting(IQueryable<Engagement> query, int sort);
        IQueryable<Project> ProjectSorting(IQueryable<Project> query, int sort);
        IQueryable<Role> RoleSorting(IQueryable<Role> query, int sort);
        IQueryable<DAL.Task> TaskSorting(IQueryable<DAL.Task> query, int sort);
        IQueryable<Team> TeamSorting(IQueryable<Team> query, int sort);
    }
}
