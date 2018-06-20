namespace TimeKeeper.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TimeKeeper.DAL.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<TimeKeeper.DAL.TimeKeeperContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TimeKeeper.DAL.TimeKeeperContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            /* List<Role> rlista = new List<Role>();

            Role r = new Role()
            {
                Name = "Software developer",
                Type = RoleType.rola1,
                Hrate = 40,
                Mrate = 1000,
            };

            rlista.Add(r);

            List<Employee> elista = new List<Employee>();

            */



            /*Employee e = new Employee()
            {
                FirstName = "Esmeralda",
                LastName = "Holjan",
                Image = null,
                Email = "esma@hotmail.com",
                Phone = "+876765388",
                BirthDate = DateTime.Now,
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Salary = 2500,
                StatusEmployee = StatusEmployee.Active,

                Days = null,
                Engagements = null,
                Role = null
            };

            context.Employees.AddOrUpdate(e);
            context.SaveChanges();

            /*


                Days = null,
                //Positions = null,
                //Roles = rlista
            };

            elista.Add(e);

            context.Employees.Add(e);
            context.SaveChanges();

            context.Roles.Add(r);
            context.SaveChanges();

            Team t = new Team()
            {
                Image = null,
                Description = "Navajo team"
            };

            context.Teams.Add(t);
            context.SaveChanges();

            Engagement en = new Engagement()
            {
                Hours = 40,
                Employee = e,
                Team = t,
                Role = r
            };

            context.Engagements.Add(en);
            context.SaveChanges();

            Address add = new Address();
            add.City = "Sarajevo";
            add.Name = "Bosmal";
            add.ZipCode = "71000";

            List<Project> plista = new List<Project>();

            Project p = new Project()
            {
                Name = "TimeKeeper",
                Description = "Application for time tracking",
                Amount = 1000,
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                StatusProject = StatusProject.InProgress,
                Pricing = Pricing.HourlyRate,
            };

            plista.Add(p);

            Customer c = new Customer()
            {
                Name = "Mistral Technologies",
                Image = null,
                Monogram = null,
                Contact = "Sulejman Catibusic",
                Email = "info@mistral.ba",
                Phone = "+38761200333",
                StatusCustomer = StatusCustomer.Client,
                Address = add,
                Projects = plista
            };

            context.Customers.Add(c);
            context.SaveChanges();

            context.Projects.Add(p);
            context.SaveChanges();


                Roles = null,
                Days = null,
                Positions = null
            };

            context.Employees.Add(e);
            context.SaveChanges();
            */
        }
    }
}
