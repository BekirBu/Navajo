using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.API.Models;
using TimeKeeper.API.Models.ReportsModel;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Repositories;

namespace TimeKeeper.API.Helper.Reports
{
    public static class ReportsHelper
    {
        //invoices
        public static List<ProjectInvoiceModel> GetInvoiceReport(this UnitOfWork unit, int year, int month, ModelFactory factory)
        {
            Random rnd = new Random();
            string inv = ("-00" + DateTime.Now.Year.ToString().Substring(2,2)).ToString();

            var listInvoices = unit.Projects.Get().SelectMany(x => x.Tasks)
                                                  .Where(x => x.Day.Date.Month == month && x.Day.Date.Year == year)
                                                  .GroupBy(y => y.Project)
                                                  .Select(w => new ProjectInvoiceModel()
                                                  {
                                                      ProjectName = w.Key.Name,
                                                      CustomerName = w.Key.Customer.Name,
                                                      Amount = w.Key.Amount,
                                                      Roles = w.Key.Team.Engagements
                                                                        .Where(t => t.Team.Id == w.Key.TeamId)
                                                                        .GroupBy(r => r.Role)
                                                                        .Select(x => new RoleInvoiceModel()
                                                                        {
                                                                            Description = x.Key.Name,
                                                                            Quantity = x.Key.Engagements
                                                                                           .Where(t => t.Team.Id == w.Key.TeamId)
                                                                                           .Select(em => em.Employee)
                                                                                           .SelectMany(d => d.Days)
                                                                                           .Where(g => g.Date.Month == month && g.Date.Year == year && g.Type == DayType.WorkingDay)
                                                                                           .SelectMany(a => a.Tasks)
                                                                                           .Where(p => p.Project.Id == w.Key.Id)
                                                                                           .Select(h => h.Hours)
                                                                                           .DefaultIfEmpty(0)
                                                                                           .Sum(),
                                                                            Unit = "Hours",
                                                                            UnitPrice = x.Key.Hrate,
                                                                            Subotal = (x.Key.Hrate) * (x.Key.Engagements
                                                                                                           .Where(t => t.Team.Id == w.Key.TeamId)
                                                                                                           .Select(em => em.Employee)
                                                                                                           .SelectMany(d => d.Days)
                                                                                                           .Where(g => g.Date.Month == month && g.Date.Year == year && g.Type == DayType.WorkingDay)
                                                                                                           .SelectMany(a => a.Tasks)
                                                                                                           .Where(p => p.Project.Id == w.Key.Id)
                                                                                                           .Select(h => h.Hours)
                                                                                                           .DefaultIfEmpty(0)
                                                                                                           .Sum())
                                                                        })
                                                                        .ToList(),
                                                      InvoiceNumber = (w.Key.Customer.Name.ToString().ToUpper().Substring(0,2) + inv).ToString(),
                                                      CustomerEmail = w.Key.Customer.Email,
                                                      MailBody = "Here is an invoice for your company."
                                                  })
                                                  .ToList();


            return listInvoices;
        }

        //missing entries
        public static List<MissingEntriesModel> GetMissingEntries(this UnitOfWork unit, 
                                                            int year, int month, ModelFactory factory)
        {
            List<MissingEntriesModel> missingEntriesList = new List<MissingEntriesModel>();
            
            int days = DateTime.DaysInMonth(year, month);
            DateTime currentDate = new DateTime(year, month, days);

            var employees = unit.Employees.Get().Where(x => x.BeginDate <= currentDate
                                                            && (x.EndDate == null
                                                            || x.EndDate > currentDate))
                                                            .ToList();

            foreach (var employee in employees)
            {
                    List<int> workingDays = DateTimeHelper.ListOfWorkingDays(year, month, 1).ToList();
                    var employeeDays = employee.Days.Where(y => y.Date.Month == month && y.Date.Year == year)
                                                        .Select(x => x.Date.Day).ToList();

                    var missing = workingDays.Except(employeeDays).ToList();

                    if (missing.Count() > 0)
                    {
                    EmployeeModel empl = new EmployeeModel()
                    {
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email
                    };

                    MissingEntriesModel emp = new MissingEntriesModel()
                    {
                        Employee = empl,
                        MissingDaysCount = Math.Abs(unit.Days.Get().Where(x => x.Employee.Id == employee.Id
                                                    && x.Date.Year == year && x.Date.Month == month)
                                                .Count() - workingDays.Count()),
                        MissingDays = missing,
                        MailBody = "You are missing an entry"

                    };

                    missingEntriesList.Add(emp);
                    }

            }

            return missingEntriesList;
        }

        public static PersonalReport GetPersonalReport(this UnitOfWork unit, int employeeId,
                                                            int year, int month, ModelFactory factory)
        {
            var emp = unit.Employees.Get().Where(x => x.Id == employeeId)
                                          .ToList()
                                          .Select(e => factory.Create(e))
                                          .FirstOrDefault();

            if (emp == null) return null;

            var days = unit.Days.Get().Where(x => x.Employee.Id == employeeId
                                                && x.Date.Year == year
                                                && x.Date.Month == month)
                                      .Select(x => new PersonalReportDays()
                                      {
                                          Date = x.Date,
                                          Type = x.Type.ToString(),
                                          Comment = x.Comment,
                                          Hours = x.Hours,
                                          OvertimeHours = x.Hours - 8,
                                          Tasks = x.Tasks.Select(y => new PersonalReportTask()
                                          {
                                              Hours = y.Hours,
                                              Description = y.Description,
                                              Project = y.Project.Name
                                          })
                                          .ToList()
                                      })
                                      .OrderBy(y => y.Date)
                                      .ToList();

            var workingDays = unit.Days.Get()
                                       .Where(x => x.Employee.Id == employeeId && x.Type == DayType.WorkingDay
                                               && x.Date.Year == year && x.Date.Month == month)
                                       .Count();

            var businessAbsence = unit.Days.Get()
                                       .Where(x => x.Employee.Id == employeeId && x.Type == DayType.BusinessAbsence
                                               && x.Date.Year == year && x.Date.Month == month)
                                       .Count();

            var otherAbsence = unit.Days.Get()
                                       .Where(x => x.Employee.Id == employeeId && x.Type == DayType.OtherAbsence
                                               && x.Date.Year == year && x.Date.Month == month)
                                       .Count();

            var publicHoliday = unit.Days.Get()
                                       .Where(x => x.Employee.Id == employeeId && x.Type == DayType.PublicHoliday
                                               && x.Date.Year == year && x.Date.Month == month)
                                       .Count();

            var religiousDay = unit.Days.Get()
                                       .Where(x => x.Employee.Id == employeeId && x.Type == DayType.ReligiousDay
                                               && x.Date.Year == year && x.Date.Month == month)
                                       .Count();

            var sickLeave = unit.Days.Get()
                                       .Where(x => x.Employee.Id == employeeId && x.Type == DayType.SickLeave
                                               && x.Date.Year == year && x.Date.Month == month)
                                       .Count();

            var vacation = unit.Days.Get()
                                       .Where(x => x.Employee.Id == employeeId && x.Type == DayType.Vacation
                                               && x.Date.Year == year && x.Date.Month == month)
                                       .Count();

            var totalHours = unit.Days.Get()
                                       .Where(x => x.Employee.Id == employeeId && x.Type == DayType.WorkingDay
                                               && x.Date.Year == year && x.Date.Month == month)
                                       .Select(x => (int?)x.Hours)
                                       .Sum() ?? 0;

            var overtimeQuery = unit.Days.Get()
                                       .Where(x => x.Employee.Id == employeeId && x.Type == DayType.WorkingDay
                                               && x.Date.Year == year && x.Date.Month == month
                                               && x.Hours > 8 );

            var workingDaysInMonth = noDaysInMonth(year,month);

            int overtimeDays = overtimeQuery.Count();

            decimal overtimeHoursSum  = overtimeQuery.Select(x => (int?)x.Hours).Sum() ?? 0;
            decimal overtimeHours = overtimeHoursSum - 8 * overtimeDays;

            int missingEntries = Math.Abs(unit.Days.Get().Where(x => x.Employee.Id == employeeId
                                                && x.Date.Year == year && x.Date.Month == month)
                                                .Count() - noDaysInMonth(year, month));

            decimal utilization = Math.Round(((decimal)workingDays / (decimal)noDaysInMonth(year, month)) * 100, 2);

            var report = new PersonalReport()
            {
                Employee = emp,
                Days = days,

                WorkingDays = workingDays,
                BusinessAbscenceDays = businessAbsence,
                OtherDays = otherAbsence,
                PublicHolidayDay = publicHoliday,
                ReligiousDays = religiousDay,
                SickLeavesDays = sickLeave,
                VacationDays = vacation,
                WorkingDaysInMonth = workingDaysInMonth,
      
                TotalHours = totalHours,
                OvertimeHours = overtimeHours,

                MissingEntries = missingEntries,
                Utilization = utilization
            };

            return report;
        }

        public static CompanyReport GetCompanyReport(this UnitOfWork unit, int year,
                                                    int month, ModelFactory factory)
        {
            int days = DateTime.DaysInMonth(year,month);
            DateTime currentDate = new DateTime(year,month,days);

            //taking into consideration employee/project who quit/ended in the given month/year
            var numEmployees = unit.Employees.Get().Where(x => x.BeginDate <= currentDate
                                                            && (x.EndDate == null
                                                            || x.EndDate > currentDate
                                                            || x.EndDate.Value.Month == month))
                                                   .Count();
            
            var numProjects = unit.Projects.Get().Where(x => x.BeginDate <= currentDate 
                                                            && (x.EndDate == null
                                                            || x.EndDate > currentDate
                                                            || x.EndDate.Value.Month == month))
                                                   .Count();

            //pm utilization
            int pmCount = unit.Employees.Get(x => x.RoleId == "MGR")
                                          .Where(x => x.BeginDate <= currentDate
                                                            && (x.EndDate == null
                                                            || x.EndDate > currentDate
                                                            || x.EndDate.Value.Month == month))
                                          .Count();

            var pmWorkingDays = unit.Days.Get(x => x.Type == DayType.WorkingDay
                                                            && x.Date.Month == month
                                                            && x.Date.Year == year)
                                            .Where(x => x.Employee.RoleId == "MGR"
                                                            && x.Employee.BeginDate <= currentDate
                                                            && (x.Employee.EndDate == null
                                                                    || x.Employee.EndDate > currentDate
                                                                    || x.Employee.EndDate.Value.Month == month))
                                            .Count();

            decimal? pmUtil = 0;
            if ((noDaysInMonth(year, month) * pmCount) != 0)
            {
                pmUtil = Math.Round((decimal)(pmWorkingDays / (decimal)(noDaysInMonth(year, month) * pmCount)) * 100, 2);
            }
            

            //qa utilization
            int qaCount = unit.Employees.Get(x => x.RoleId == "QAE")
                              .Where(x => x.BeginDate <= currentDate
                                                && (x.EndDate == null
                                                || x.EndDate > currentDate
                                                || x.EndDate.Value.Month == month))
                              .Count();

            var qaWorkingDays = unit.Days.Get(x => x.Type == DayType.WorkingDay
                                                            && x.Date.Month == month
                                                            && x.Date.Year == year)
                                            .Where(x => x.Employee.RoleId == "QAE"
                                                            && x.Employee.BeginDate <= currentDate
                                                            && (x.Employee.EndDate == null
                                                                    || x.Employee.EndDate > currentDate
                                                                    || x.Employee.EndDate.Value.Month == month))
                                            .Count();

            decimal qaUtil = 0;
            if (noDaysInMonth(year, month) * qaCount != 0)
            {
                qaUtil = Math.Round((decimal)(qaWorkingDays / (decimal)(noDaysInMonth(year, month) * qaCount)) * 100, 2);
            }
            

            //dev utilization
            int devCount = unit.Employees.Get(x => x.RoleId == "DEV")
                              .Where(x => x.BeginDate <= currentDate
                                                && (x.EndDate == null
                                                || x.EndDate > currentDate
                                                || x.EndDate.Value.Month == month))
                              .Count();

            var devWorkingDays = unit.Days.Get(x => x.Type == DayType.WorkingDay
                                                            && x.Date.Month == month
                                                            && x.Date.Year == year)
                                            .Where(x => x.Employee.RoleId == "DEV"
                                                            && x.Employee.BeginDate <= currentDate
                                                            && (x.Employee.EndDate == null
                                                                    || x.Employee.EndDate > currentDate
                                                                    || x.Employee.EndDate.Value.Month == month))
                                            .Count();

            decimal devUtil = 0;
            if (noDaysInMonth(year, month) * devCount != 0)
            {
                devUtil = Math.Round((decimal)(devWorkingDays / (decimal)(noDaysInMonth(year, month) * devCount)) * 100, 2);
            }
            

            //uiux utilization
            int uiuxCount = unit.Employees.Get(x => x.RoleId == "UIX")
                                          .Where(x => x.BeginDate <= currentDate
                                                            && (x.EndDate == null
                                                            || x.EndDate > currentDate
                                                            || x.EndDate.Value.Month == month))
                                          .Count();

            var uiuxWorkingDays = unit.Days.Get(x => x.Type == DayType.WorkingDay
                                                            && x.Date.Month == month
                                                            && x.Date.Year == year)
                                            .Where(x => x.Employee.RoleId == "UIX"
                                                            && x.Employee.BeginDate <= currentDate
                                                            && (x.Employee.EndDate == null
                                                                    || x.Employee.EndDate > currentDate
                                                                    || x.Employee.EndDate.Value.Month == month))
                                            .Count();

            decimal uiuxUtil = 0;
            if (noDaysInMonth(year, month) * uiuxCount != 0)
            {
                uiuxUtil = Math.Round((decimal)(uiuxWorkingDays / (decimal)(noDaysInMonth(year, month) * uiuxCount)) * 100, 2);
            }

            //total working hours for every employee
            var totalHours = unit.Days.Get()
                                       .Where(x => x.Date.Year == year && x.Date.Month == month)
                                       .Select(x => (int?)x.Hours)
                                       .Sum() ?? 0;

            //var totalHours = unit.Teams.Get().SelectMany(x => x.Engagements).Select(x => x.Employee)
            //                                                       .SelectMany(x => x.Days)
            //                                                       .Where(x => x.Date.Month == month && x.Date.Year == year && x.Type == DayType.WorkingDay)
            //                                                       .Select(y => (int?)y.Hours)
            //                                                       .Sum() ?? 0;

            int maxPossibleTotalHours = noDaysInMonth(year, month) * 8 * numEmployees;

            int missingEntriesTotal = 0;
            int daysInMonth = noDaysInMonth(year, month);
            var allTeams = unit.Teams.Get().ToList();

            List<CompanyReportTeams> overtimeTeams = new List<CompanyReportTeams>();

            foreach (var team in allTeams)
            {
                decimal overtimehours = 0;
                var employees = unit.Engagements.Get().Where(x => x.Team.Id == team.Id).Select(x => x.Employee).ToList();
                var projects = team.Projects;
                
                int missingEntriesTeam = 0;
                decimal hoursTeam = 0;
                decimal hoursTeam1 = 0;

                foreach (var employee in employees)
                {
                    decimal hoursEmp = 0;
                    int daysCount = 0;

                    decimal totHours = 0;
                    foreach(var project in projects)
                    {
                        var hoursPro = unit.Days.Get().Where(x => x.Date.Month == month && x.Date.Year == year && employee.Id == x.Employee.Id)
                                             .SelectMany(t => t.Tasks)
                                             .Where(x => x.Project.Id == project.Id)
                                             .Select(h => h.Hours)
                                             .DefaultIfEmpty(0)
                                             .Sum();
                        totHours += hoursPro;
                    }

                    hoursTeam1 += totHours;

                    foreach (var day in employee.Days)
                    {
                        if (day.Hours > 8 && day.Date.Month == month && day.Date.Year == year && day.Type == DayType.WorkingDay)
                        {
                            overtimehours += day.Hours - 8;
                        }

                        if (day.Date.Month == month && day.Date.Year == year && day.Type == DayType.WorkingDay)
                        {
                            hoursEmp += day.Hours;
                            daysCount++;
                        }
                    }

                    hoursTeam += hoursEmp;
                    missingEntriesTeam += (daysInMonth - daysCount);
                }

                int maxHours = noDaysInMonth(year, month) * employees.Count() * 8;
                var teamUtil = Math.Round(((double)hoursTeam1 / maxHours) * 100, 2);

                CompanyReportTeams teamToAdd = new CompanyReportTeams()
                {
                    TeamName = team.Name,
                    OvertimeHours = overtimehours,
                    TeamMissingHours = missingEntriesTeam,
                    TeamHours = hoursTeam1,
                    Utilization = teamUtil
                };

                missingEntriesTotal += missingEntriesTeam;
                overtimeTeams.Add(teamToAdd);
            }

            var allProjects = unit.Projects.Get().Where(x => x.BeginDate <= currentDate
                                                            && (x.EndDate == null
                                                            || x.EndDate > currentDate
                                                            || x.EndDate.Value.Month == month))
                                                 .ToList();

            List<CompanyReportProjects> revenueProjects = new List<CompanyReportProjects>();

            foreach(var project in allProjects)
            {
                CompanyReportProjects projectToAdd = new CompanyReportProjects()
                {
                    ProjectName = project.Name,
                    Revenue = project.Amount
                };

                revenueProjects.Add(projectToAdd);
            }
            
            //creating a report
            var companyReport = new CompanyReport
            {
                NumEmployees = numEmployees,
                NumProjects = numProjects,
                TotalHours = totalHours,
                MaxPossibleTotalHours = maxPossibleTotalHours,
                MissingEntries = missingEntriesTotal,

                PMUtilization = pmUtil.Value,
                PMCount = pmCount,

                DEVUtilization = devUtil,
                DEVCount = devCount,

                QAUtilization = qaUtil,
                QACount = qaCount,

                UIUXUtilization = uiuxUtil,
                UIUXCount = uiuxCount,
                
                OvertimeHoursTeams = overtimeTeams,
                RevenueProjects = revenueProjects

            };

            return companyReport;
        }

        public static int noDaysInMonth(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            int daysInMonth = 0;

            for (int i = 1; i <= days; i++)
            {
                DateTime day = new DateTime(year, month, i);
                if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
                {
                    daysInMonth++;
                }
            }

            return daysInMonth;
        }

        public static int noDaysInYear(int year)
        {
            DateTime start = new DateTime(year, 1, 1);
            DateTime end = new DateTime(year, 12, 31);

            int daysInYear = 0;
            while (start <= end)
            {
                if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++daysInYear;
                }
                start = start.AddDays(1);
            }

            return daysInYear;
        }

        public static TeamReport GetTeamReport(this UnitOfWork unit, string teamId, int year, int month, ModelFactory factory)
        {
            var employees = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Select(x => x.Employee).ToList();
            List<TeamMemberModel> reports = new List<TeamMemberModel>();
            decimal? hours = 0;
            int maxPossibleDays = noDaysInMonth(year, month);

            DayStatisticModel statictic = new DayStatisticModel()
            {
                BusinessAbscenceDays = 0,
                MissingEntries = 0,
                OtherAbscenceDays = 0,
                OvertimeHours = 0,
                PercentageOfWorkingDays = 0,
                PublicHolidays = 0,
                ReligiousDays = 0,
                SickLeaveDays = 0,
                VacationDays = 0,
                WorkingDays = 0,
                MaxPossibleWorkingDays = maxPossibleDays

            };

            var projects = unit.Projects.Get().ToList();

            foreach (var employee in employees)
            {
                decimal hoursTeam1 = 0;
                var days = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Select(x => x.Employee).SelectMany(y => y.Days)
                    .Where(x => x.Employee.Id == employee.Id
                                                    && x.Date.Year == year
                                                    && x.Date.Month == month);
                List<int> listDates = null;
                var date = new DateTime(year, month, 1);

                if (employee.BeginDate.Year == date.Year && employee.BeginDate.Month == date.Month
                                                    && employee.BeginDate.Day < DateTime.DaysInMonth(year, month))
                {
                    listDates = DateTimeHelper.ListOfWorkingDays(year, month, employee.BeginDate.Day).ToList();
                }
                else
                {
                    listDates = DateTimeHelper.ListOfWorkingDays(year, month).ToList();
                }

                int workingDays = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Select(x => x.Employee).SelectMany(y => y.Days)
                                        .Where(x => x.Employee.Id == employee.Id
                                                    && x.Date.Year == year
                                                    && x.Date.Month == month
                                                    && x.Type == DayType.WorkingDay).Count();

                statictic.WorkingDays += workingDays;

                //novoovovo

                //decimal totHours = 0;
                //foreach (var project in projects)
                //{
                //    var hoursPro = unit.Days.Get().Where(x => x.Date.Month == month && x.Date.Year == year).SelectMany(t => t.Tasks)
                //                         .Where(x => x.Project.Id == project.Id)
                //                         .Select(h => h.Hours)
                //                         .DefaultIfEmpty(0)
                //                         .Sum();
                //    totHours += hoursPro;
                //}

                //hoursTeam1 += totHours;


                decimal? overtime = 0;

                foreach (var day in days)
                {
                    if (day.Hours > 8)
                    {
                        overtime += day.Hours - 8;

                    };
                };

                statictic.OvertimeHours += overtime;

                int vacationDays = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Select(x => x.Employee).SelectMany(y => y.Days).Where(x => x.Employee.Id == employee.Id
                                                         && x.Date.Year == year
                                                         && x.Date.Month == month
                                                         && x.Type == DayType.Vacation).Count();
                statictic.VacationDays += vacationDays;

                int businessAbscenceDays = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Select(x => x.Employee).SelectMany(y => y.Days).Where(x => x.Employee.Id == employee.Id
                                                         && x.Date.Year == year
                                                         && x.Date.Month == month
                                                         && x.Type == DayType.BusinessAbsence).Count();

                statictic.BusinessAbscenceDays += businessAbscenceDays;

                int publicHolidays = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Select(x => x.Employee).SelectMany(y => y.Days).Where(x => x.Employee.Id == employee.Id
                                                         && x.Date.Year == year
                                                         && x.Date.Month == month
                                                         && x.Type == DayType.PublicHoliday).Count();

                statictic.PublicHolidays += publicHolidays;

                int sickLeaveDays = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Select(x => x.Employee).SelectMany(y => y.Days).Where(x => x.Employee.Id == employee.Id
                                                         && x.Date.Year == year
                                                         && x.Date.Month == month
                                                         && x.Type == DayType.SickLeave).Count();

                statictic.SickLeaveDays += sickLeaveDays;

                int religiousDays = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Select(x => x.Employee).SelectMany(y => y.Days).Where(x => x.Employee.Id == employee.Id
                                                         && x.Date.Year == year
                                                         && x.Date.Month == month
                                                         && x.Type == DayType.ReligiousDay).Count();

                statictic.ReligiousDays += religiousDays;

                int otherAbsenceDays = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Select(x => x.Employee).SelectMany(y => y.Days).Where(x => x.Employee.Id == employee.Id
                                                         && x.Date.Year == year
                                                         && x.Date.Month == month
                                                         && x.Type == DayType.OtherAbsence).Count();

                statictic.OtherAbscenceDays += otherAbsenceDays;

                decimal? totalhoursss = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Select(x => x.Employee).SelectMany(y => y.Days).Where(x => x.Employee.Id == employee.Id
                                                        && x.Date.Year == year
                                                        && x.Date.Month == month && x.Type == DayType.WorkingDay)
                                                        .Select(x => x.Hours).DefaultIfEmpty(0).Sum();

                var missingEntries = (employee.BeginDate.Date > date.Date) ? 0 : listDates.Except(days.Select(x => x.Date.Day)).Count();

                statictic.MissingEntries += missingEntries;

                TeamMemberModel employeeModel = new TeamMemberModel()
                {
                    Employee = new BaseModel()
                    {
                        Id = employee.Id,
                        Name = employee.FirstName + ' ' + employee.LastName
                    },
                    Days = new DayStatisticModel()
                    {
                        WorkingDays = workingDays,
                        VacationDays = vacationDays,
                        BusinessAbscenceDays = businessAbscenceDays,
                        PublicHolidays = publicHolidays,
                        SickLeaveDays = sickLeaveDays,
                        ReligiousDays = religiousDays,
                        OtherAbscenceDays = otherAbsenceDays,
                        OvertimeHours = overtime,
                        PercentageOfWorkingDays = Math.Round(100 * (double)workingDays / listDates.Count(), 2),
                        MissingEntries = missingEntries,
                        MaxPossibleWorkingDays = maxPossibleDays

                    },
                    TotalHours = totalhoursss
                    //TotalHours = hoursTeam1
                };

                reports.Add(employeeModel);
                hours += employeeModel.TotalHours;
            }

            var utilization = Math.Round(((decimal)statictic.WorkingDays / (decimal)noDaysInMonth(year, month)) * 100, 2);

            int numberOfEmployees = unit.Engagements.Get().Where(x => x.Team.Id == teamId).Count();
            var fullworkingdays = numberOfEmployees * DateTimeHelper.ListOfWorkingDays(year, month).Count();
            statictic.PercentageOfWorkingDays = Math.Round(100 * (double)statictic.WorkingDays / fullworkingdays);
            
            var report = new TeamReport()
            {
                Id = teamId,
                Name = unit.Teams.Get(teamId).Name,
                NumberOfEmployees = numberOfEmployees,
                NumberOfProjects = unit.Teams.Get(teamId).Projects.Count(),
                Days = statictic,
                Reports = reports,
                TotalHours = hours,
                Year = year,
                Month = month,
                Utilization = utilization
            };
            return report;
        }

        public static List<MonthlyReport> GetMonthlyReport(this UnitOfWork unit, int year, 
                                                            int month, ModelFactory factory)
        {
            var report = unit.Employees.Get()
                           .SelectMany(t => t.Engagement)
                           .GroupBy(x => x.Employee)
                           .ToList()
                           .Select(w => new MonthlyReport()
                           {
                               Employee = factory.Create(w.Key),
                               Projects = w.Key.Engagement.Select(x => x.Team)
                                                          .SelectMany(x => x.Projects)
                                                          .ToList()
                                                          .Select(x => factory.CreateMonthlyReportProject(x, w.Key.Id, month, year))
                                                          .ToList(),
                               TotalWorkingHours = w.Key.Engagement.Select(y => y.Team)
                                                                   .SelectMany(p => p.Projects)
                                                                   .SelectMany(t => t.Tasks)
                                                                   .Select(d => d.Day)
                                                                   .Where(x => x.Type == DayType.WorkingDay && x.Date.Month == month
                                                                                && x.Date.Year == year
                                                                                && x.Employee.Id == w.Key.Id)
                                                                   .Select(h => h.Hours)
                                                                   .Sum()
                           })
                           .ToList();

            return report;
        }

        public static List<AnnualReport> GetAnnualOverview(this UnitOfWork unit, int year, ModelFactory factory)
        {
            var query = unit.Projects.Get().OrderBy(x => x.Name)
                                               .Select(x => new
                                               {
                                                   project = x.Name,
                                                   details = x.Tasks.Where(d => d.Day.Date.Year == year)
                                                                      .GroupBy(d => d.Day.Date.Month)
                                                                      .Select(w => new { month = w.Key, hours = w.Sum(d => d.Hours) })
                                                                      .ToList()
                                               }).ToList();

            List<AnnualReport> list = new List<AnnualReport>();
            AnnualReport total = new AnnualReport { ProjectName = "T O T A L" };

            foreach (var q in query)
            {
                AnnualReport item = new AnnualReport { ProjectName = q.project };
                foreach (var w in q.details)
                {
                    item.TotalHours += w.hours;
                    total.TotalHours += w.hours;
                    item.MonthlyHours[w.month - 1] = w.hours;
                    total.MonthlyHours[w.month - 1] += w.hours;
                }
                if (item.TotalHours > 0) list.Add(item);
            }
            list.Add(total);
            return list;
        }

        public static HistoryReport GetProjectHistory(this UnitOfWork unit, int id, ModelFactory factory)
        {
            decimal? monthlyRate = unit.Projects.Get(id).Amount;
            HistoryReport report = new HistoryReport();
            List<HistoryReportEmployees> ReportEmployees = new List<HistoryReportEmployees>();

            var project = unit.Projects.Get(id);

            List<int> years = new List<int>
            {
                2015,
                2016,
                2017,
                2018
            };
            
            var employees = unit.Projects.Get().Where(x => x.Id == id)
                                               .Select(y => y.Team)
                                               .SelectMany(z => z.Engagements)
                                               .Select(y => y.Employee)
                                               .ToList();

            foreach (var employee in employees)
            {
                HistoryReportEmployees empToAdd = new HistoryReportEmployees();

                empToAdd.Id = employee.Id;
                empToAdd.Name = employee.FirstName + " " + employee.LastName;


                var totalHours = unit.Days.Get().Where(x => x.Employee.Id == employee.Id)
                                                .SelectMany(y => y.Tasks)
                                                .Where(x => x.Project.Id == id)
                                                .Select(x => x.Hours)
                                                .DefaultIfEmpty(0)
                                                .Sum();

                empToAdd.TotalHours = totalHours;

                foreach (var year in years)
                {
                    HistoryReportTotal empvar = new HistoryReportTotal()
                    {
                        Year = year,
                        Sum = unit.Days.Get().Where(x => x.Employee.Id == employee.Id 
                                                                    && x.Date.Year == year)
                                                            .SelectMany(y => y.Tasks)
                                                            .Where(x => x.Project.Id == id)
                                                            .Select(x => x.Hours)
                                                            .DefaultIfEmpty(0).Sum()
                    };

                    empToAdd.SumList.Add(empvar);
                }
                ReportEmployees.Add(empToAdd);
            }
            report.Employees = ReportEmployees;

            var projectTotalHours = unit.Days.Get().SelectMany(y => y.Tasks)
                                                   .Where(x => x.Project.Id == id)
                                                   .Select(x => x.Hours)
                                                   .DefaultIfEmpty(0)
                                                   .Sum();

            report.TotalHours = projectTotalHours;

            return report;
        }
        
    }
}