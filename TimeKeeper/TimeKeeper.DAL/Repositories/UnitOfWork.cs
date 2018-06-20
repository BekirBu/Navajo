﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly TimeKeeperContext context = new TimeKeeperContext();
        private IRepository<Category, int> _categories;
        private IRepository<Customer, int> _customers;
        private IRepository<Day, int> _days;
        private EmployeeRepository _employees;
        private IRepository<Engagement, int> _engagements;
        private IRepository<Project, int> _projects;
        private IRepository<Role, string> _roles;
        private IRepository<Task, int> _tasks;
        private IRepository<Team, string> _teams;

        public IRepository<Category, int> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new Repository<Category, int>(context);
                }
                return _categories;
            }
        }

        public IRepository<Customer, int> Customer
        {
            get
            {
                if (_customers == null)
                {
                    _customers = new Repository<Customer, int>(context);
                }
                return _customers;
            }
        }

        public IRepository<Day, int> Days
        {
            get
            {
                if (_days == null)
                {
                    _days = new Repository<Day, int>(context);
                }
                return _days;
            }
        }

        public IRepository<Employee, int> Employees
        {
            get
            {
                if (_employees == null)
                {
                    _employees = new EmployeeRepository(context);
                }
                return _employees;
            }
        }

        public IRepository<Engagement, int> Engagements
        {
            get
            {
                if (_engagements == null)
                {
                    _engagements = new Repository<Engagement, int>(context);
                }
                return _engagements;
            }
        }

        public IRepository<Project, int> Projects
        {
            get
            {
                if (_projects == null)
                {
                    _projects = new Repository<Project, int>(context);
                }
                return _projects;
            }
        }

        public IRepository<Role, string> Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new Repository<Role, string>(context);
                }
                return _roles;
            }
        }

        public IRepository<Task, int> Tasks
        {
            get
            {
                if (_tasks == null)
                {
                    _tasks = new Repository<Task, int>(context);
                }
                return _tasks;
            }
        }

        public IRepository<Team, string> Teams
        {
            get
            {
                if (_teams == null)
                {
                    _teams = new Repository<Team, string>(context);
                }
                return _teams;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public bool Save()
        {
            return (context.SaveChanges() > 0);
        }
    }
}
