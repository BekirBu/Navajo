using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Helper
{
    public class Filtering : IFiltering
    {
        public IQueryable<Customer> CustomerFiltering(IQueryable<Customer> query, string filter)
        {
            if (filter != "") query = query.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
            return query;
        }

        public IQueryable<Day> DayFiltering(IQueryable<Day> query, string filter)
        {
            if (filter != "") query = query.Where(x => x.Type.ToString().Contains(filter.ToLower()));
            return query;
        }

        public IQueryable<Employee> EmployeeFiltering(IQueryable<Employee> query, string filter)
        {
            if (filter != "") query = query.Where(x => x.LastName.ToLower().Contains(filter.ToLower())
                                                    || x.FirstName.ToLower().Contains(filter.ToLower()));
            return query;
        }

        public IQueryable<Engagement> EngagementFiltering(IQueryable<Engagement> query, string filter)
        {
            if (filter != "") query = query.Where(x => x.Team.Name.ToString().Contains(filter.ToLower()));
            return query;
        }

        public IQueryable<Project> ProjectFiltering(IQueryable<Project> query, string filter)
        {
            if (filter != "") query = query.Where(x => x.Name.Contains(filter.ToLower()));
            return query;
        }

        public IQueryable<Role> RoleFiltering(IQueryable<Role> query, string filter)
        {
            if (filter != "") query = query.Where(x => x.Name.Contains(filter.ToLower()));
            return query;
        }

        public IQueryable<Task> TaskFiltering(IQueryable<Task> query, string filter)
        {
            if (filter != "") query = query.Where(x => x.Project.Name.Contains(filter.ToLower()));
            return query;
        }

        public IQueryable<Team> TeamFiltering(IQueryable<Team> query, string filter)
        {
            if (filter != "") query = query.Where(x => x.Name.Contains(filter.ToLower()));
            return query;
        }
    }
}