using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Helper
{
    interface IPaging
    {
        List<CustomerModel> CustomerPaging(IQueryable<Customer> query, int page, int pageSize);
        //List<DayModel> DayPaging(IQueryable<Day> query, int page, int pageSize);
        List<EmployeeModel> EmployeePaging(IQueryable<Employee> query, int page, int pageSize);
        List<EngagementModel> EngagementPaging(IQueryable<Engagement> query, int page, int pageSize);
        List<ProjectModel> ProjectPaging(IQueryable<Project> query, int page, int pageSize);
        List<RoleModel> RolePaging(IQueryable<Role> query, int page, int pageSize);
        List<DetailModel> TaskPaging(IQueryable<DAL.Task> query, int page, int pageSize);
        List<TeamModel> TeamPaging(IQueryable<Team> query, int page, int pageSize);
    }
}
