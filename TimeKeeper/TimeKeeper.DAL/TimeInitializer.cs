using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repositories;

namespace TimeKeeper.DAL
{
    internal class TimeInitializer<T> : DropCreateDatabaseAlways<TimeKeeperContext>
    {
        public override void InitializeDatabase(TimeKeeperContext context)
        {
            try
            {
                // ensure that old database instance can be dropped
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                        $"ALTER DATABASE {context.Database.Connection.Database} SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            }
            catch
            {
                // database does not exists - no problem ;o)
            }
            finally
            {
                base.InitializeDatabase(context);

                using (UnitOfWork unit = new UnitOfWork())
                {
                    addRoles(unit);
                    addTeams(unit);
                    addEmployees(unit);
                    addEngagement(unit);

                    addCustomer(unit);
                    addProjects(unit);

                    addDays(unit);
                    addTasks(unit);
                }

            }
        }

        void addRoles(UnitOfWork unit)
        {
            unit.Roles.Insert(new Role()
            {
                Id = "SD",
                Name = "Software Developer",
                Type = RoleType.TeamRole,
                Hrate = 30,
                Mrate = 4500
            });
            unit.Roles.Insert(new Role()
            {
                Id = "UX",
                Name = "UI/UX Designer",
                Type = RoleType.TeamRole,
                Hrate = 45,
                Mrate = 6500
            });
            unit.Save();
        }

        void addTeams(UnitOfWork unit)
        {
            unit.Teams.Insert(new Team()
            {
                Name = "Alpha",
                Id = "A",
                Image = "A",
                Description = "Alpha Team"
            });
            unit.Teams.Insert(new Team()
            {
                Name = "Bravo",
                Id = "B",
                Image = "B",
                Description = "Bravo Team"
            });
            unit.Save();
        }

        void addEmployees(UnitOfWork unit)
        {
            unit.Employees.Insert(new Employee()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@gmail.com",
                Image = "JohnDoe",
                Phone = "65465465",
                BirthDate = DateTime.Now.AddYears(-32),
                StatusEmployee = StatusEmployee.Active,
                BeginDate = DateTime.Now.AddYears(-1),
                Salary = 2200,
                Position = unit.Roles.Get("SD"),
                EndDate = DateTime.Now

            });
            unit.Employees.Insert(new Employee()
            {
                FirstName = "Hazim",
                LastName = "Mizah",
                Email = "mail1@gmail.com",
                Image = "hazimM",
                Phone = "6546345465",
                BirthDate = DateTime.Now.AddYears(-22),
                StatusEmployee = StatusEmployee.Active,
                BeginDate = DateTime.Now.AddYears(-2),
                Salary = 1500,
                Position = unit.Roles.Get("SD"),
                EndDate = DateTime.Now

            });
            unit.Employees.Insert(new Employee()
            {
                FirstName = "Hamida",
                LastName = "Hamic",
                Email = "mail@gmail.com",
                Image = "HamidaH",
                Phone = "65465465",
                BirthDate = DateTime.Now.AddYears(-20),
                StatusEmployee = StatusEmployee.Active,
                BeginDate = DateTime.Now.AddYears(-1),
                Salary = 2500,
                Position = unit.Roles.Get("SD"),
                EndDate = DateTime.Now
            });
            unit.Save();
        }

        void addEngagement(UnitOfWork unit)
        {
            unit.Engagements.Insert(new Engagement()
            {
                Hours = 40,
                Employee = unit.Employees.Get(1),
                Team = unit.Teams.Get("A"),
                Role = unit.Roles.Get("SD")

            });
            unit.Engagements.Insert(new Engagement()
            {
                Hours = 40,
                Employee = unit.Employees.Get(2),
                Team = unit.Teams.Get("B"),
                Role = unit.Roles.Get("UX")

            });
            unit.Save();
        }


        void addCustomer(UnitOfWork unit)
        {

            Address add = new Address
            {
                City = "Sarajevo",
                Name = "Bosmal",
                ZipCode = "71000"
            };

            unit.Customer.Insert(new Customer()
            {
                Image = "image",
                Monogram = "BHT",
                Name = "BH Telekom",
                Contact = "Rambo Amadeus",
                Address = add,
                Email = "telekom@mail.com",
                Phone = "654654654",
                StatusCustomer = StatusCustomer.Client


            });
            unit.Customer.Insert(new Customer()
            {
                Image = "image",
                Monogram = "CT",
                Name = "ComTrade",
                Contact = "Nazif Gljiva",
                Address = add,
                Email = "comtrade@mail.com",
                Phone = "654654654",
                StatusCustomer = StatusCustomer.Prospect
            });
            unit.Save();
        }

        void addProjects(UnitOfWork unit)
        {
            unit.Projects.Insert(new Project()
            {
                Name = "Intranet",
                BeginDate = DateTime.Now.AddYears(-2),
                Monogram = "IN",
                Amount = 20000,
                Customer = unit.Customer.Get(1),
                Description = "Company managment application|",
                Pricing = Pricing.FixedRate,
                StatusProject = StatusProject.InProgress,
                Team = unit.Teams.Get("A")

            });
            unit.Projects.Insert(new Project()
            {
                Name = "Timekeeper",
                BeginDate = DateTime.Now.AddYears(-1),
                Monogram = "TK",
                Amount = 1500,
                Customer = unit.Customer.Get(2),
                Description = "Employee time keeping application|",
                Pricing = Pricing.FixedRate,
                StatusProject = StatusProject.InProgress,
                Team = unit.Teams.Get("B")

            });
            unit.Save();
        }

        void addDays(UnitOfWork unit)
        {
            unit.Days.Insert(new Day()
            {
                Hours = 8,
                Comment = "This is a working day",
                Type = DayType.WorkingDay,
                Date = DateTime.Now,
                Employee = unit.Employees.Get(1)

            });
            unit.Days.Insert(new Day()
            {
                Hours = 0,
                Comment = "This is a vacation day",
                Type = DayType.Vacation,
                Date = DateTime.Now,
                Employee = unit.Employees.Get(2)

            });
            unit.Save();
        }

        void addTasks(UnitOfWork unit)
        {
            unit.Tasks.Insert(new Task()
            {
                Day = unit.Days.Get(1),
                Description = "Simple maintenance task",
                Hours = 2,
                Project = unit.Projects.Get(1)

            });
            unit.Tasks.Insert(new Task()
            {
                Day = unit.Days.Get(2),
                Description = "Bug fixes",
                Hours = 6,
                Project = unit.Projects.Get(2)

            });
            unit.Save();
        }
    }
}