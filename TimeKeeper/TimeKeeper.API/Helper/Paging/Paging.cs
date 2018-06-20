using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Repositories;

namespace TimeKeeper.API.Helper
{
    public class Paging : BaseController, IPaging
    {
        public List<CustomerModel> CustomerPaging(IQueryable<Customer> query,
                                            int page, int pageSize)
        {
            int itemCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            var list = query.Skip(pageSize * page)
                             .Take(pageSize)
                             .ToList()
                             .Select(t => TimeFactory.Create(t))
                             .ToList();

            return list;
        }

        //public List<DayModel> DayPaging(IQueryable<Day> query,
        //                            int page, int pageSize)
        //{
        //    int itemCount = query.Count();
        //    int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

        //    var list = query.Skip(pageSize * page)
        //                     .Take(pageSize)
        //                     .ToList()
        //                     .Select(t => TimeFactory.Create(t))
        //                     .ToList();

        //    return list;
        //}

        public List<EmployeeModel> EmployeePaging(IQueryable<Employee> query,
                                            int page, int pageSize)
        {
            int itemCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            var list = query.Skip(pageSize * page)
                             .Take(pageSize)
                             .ToList()
                             .Select(t => TimeFactory.Create(t))
                             .ToList();

            return list;
        }

        public List<EngagementModel> EngagementPaging(IQueryable<Engagement> query,
                            int page, int pageSize)
        {
            int itemCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            var list = query.Skip(pageSize * page)
                             .Take(pageSize)
                             .ToList()
                             .Select(t => TimeFactory.Create(t))
                             .ToList();

            return list;
        }

        public List<ProjectModel> ProjectPaging(IQueryable<Project> query,
                    int page, int pageSize)
        {
            int itemCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            var list = query.Skip(pageSize * page)
                             .Take(pageSize)
                             .ToList()
                             .Select(t => TimeFactory.Create(t))
                             .ToList();

            return list;
        }

        public List<RoleModel> RolePaging(IQueryable<Role> query,
            int page, int pageSize)
        {
            int itemCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            var list = query.Skip(pageSize * page)
                             .Take(pageSize)
                             .ToList()
                             .Select(t => TimeFactory.Create(t))
                             .ToList();

            return list;
        }

        public List<DetailModel> TaskPaging(IQueryable<Task> query,
            int page, int pageSize)
        {
            int itemCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            var list = query.Skip(pageSize * page)
                             .Take(pageSize)
                             .ToList()
                             .Select(t => TimeFactory.Create(t))
                             .ToList();

            return list;
        }

        public List<TeamModel> TeamPaging(IQueryable<Team> query,
            int page, int pageSize)
        {
            int itemCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            var list = query.Skip(pageSize * page)
                             .Take(pageSize)
                             .ToList()
                             .Select(t => TimeFactory.Create(t))
                             .ToList();

            return list;
        }
    }

}