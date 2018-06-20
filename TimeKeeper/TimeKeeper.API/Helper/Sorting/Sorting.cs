using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Helper
{
    public class Sorting : ISorting
    {
        public IQueryable<Customer> CustomerSorting(IQueryable<Customer> query, int sort)
        {
            switch (sort)
            {
                case 1: query = query.OrderBy(x => x.Name); break;
                case 2: query = query.OrderBy(x => x.StatusCustomer); break;
                case 3: query = query.OrderBy(x => x.Address.City); break;
                default: query = query.OrderBy(x => x.Id); break;
            }

            return query;
        }
        
        public IQueryable<Day> DaySorting(IQueryable<Day> query, int sort)
        {
            switch (sort)
            {
                case 1: query = query.OrderBy(x => x.Employee.LastName); break;
                case 2: query = query.OrderBy(x => x.Type); break;
                default: query = query.OrderBy(x => x.Id); break;
            }

            return query;
        }

        public IQueryable<Employee> EmployeeSorting(IQueryable<Employee> query, int sort)
        {
            switch (sort)
            {
                case 1: query = query.OrderBy(x => x.FirstName); break;
                case 2: query = query.OrderBy(x => x.LastName); break;
                case 3: query = query.OrderBy(x => x.Position.Name); break;
                case 4: query = query.OrderBy(x => x.StatusEmployee); break;
                case 5: query = query.OrderBy(x => x.BeginDate.Year); break;
                default: query = query.OrderBy(x => x.Id); break;
            }

            return query;
        }

        public IQueryable<Engagement> EngagementSorting(IQueryable<Engagement> query, int sort)
        {
            switch (sort)
            {
                case 1: query = query.OrderBy(x => x.Team.Name); break;
                case 2: query = query.OrderBy(x => x.Role.Name); break;
                case 3: query = query.OrderBy(x => x.Employee.FirstName); break;
                default: query = query.OrderBy(x => x.Id); break;
            }

            return query;
        }

        public IQueryable<Project> ProjectSorting(IQueryable<Project> query, int sort)
        {
            switch (sort)
            {
                case 1: query = query.OrderBy(x => x.Name); break;
                case 2: query = query.OrderBy(x => x.StatusProject); break;
                case 3: query = query.OrderBy(x => x.Pricing); break;
                case 4: query = query.OrderBy(x => x.Amount); break;
                case 5: query = query.OrderBy(x => x.Customer.Name); break;
                case 6: query = query.OrderBy(x => x.Team.Name); break;
                case 7: query = query.OrderBy(x => x.BeginDate.Year); break;
                default: query = query.OrderBy(x => x.Id); break;
            }

            return query;
        }

        public IQueryable<Role> RoleSorting(IQueryable<Role> query, int sort)
        {
            switch (sort)
            {
                case 1: query = query.OrderBy(x => x.Name); break;
                case 2: query = query.OrderBy(x => x.Hrate); break;
                case 3: query = query.OrderBy(x => x.Mrate); break;
                case 4: query = query.OrderBy(x => x.Type); break;
                default: query = query.OrderBy(x => x.Id); break;
            }

            return query;
        }

        public IQueryable<Task> TaskSorting(IQueryable<Task> query, int sort)
        {
            switch (sort)
            {
                case 1: query = query.OrderBy(x => x.Project.Name); break;
                case 2: query = query.OrderBy(x => x.Day.Date); break;
                default: query = query.OrderBy(x => x.Id); break;
            }

            return query;
        }

        public IQueryable<Team> TeamSorting(IQueryable<Team> query, int sort)
        {
            switch (sort)
            {
                case 1: query = query.OrderBy(x => x.Name); break;
                default: query = query.OrderBy(x => x.Id); break;
            }

            return query;
        }
    }
}