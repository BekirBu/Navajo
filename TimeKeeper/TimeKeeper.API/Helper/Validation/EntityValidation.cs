using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Helper.Validation
{
    static public class EntityValidation
    {
        public static List<string> Validate (this Customer customer)
        {
            var errors = new List<string>();

            if (customer.StatusCustomer != StatusCustomer.Client
                    && customer.StatusCustomer != StatusCustomer.Prospect)
            {
                errors.Add("Customer can be client or prospect.");
            }

            if (customer.Name.Length < 2)
            {
                errors.Add("Name must be longer than 2 characters.");
            }

            EmailValidation email = new EmailValidation();

            if (email.IsValidEmail(customer.Email) == false)
            {
                errors.Add("Wrong email.");
            }

            var regexName = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexName.IsMatch(customer.Name))
            {
                errors.Add("Name can't contain special characters.");
            }

            var regexPhone = new Regex("^[0-9+() ]*$");

            if (!regexPhone.IsMatch(customer.Phone))
            {
                errors.Add("Phone can't have letters.");
            }

            return errors;
        }

        public static List<string> Validate(this Day day)
        {
            var errors = new List<string>();

            if (day.Type != DayType.WorkingDay
                    && day.Type != DayType.Vacation
                    && day.Type != DayType.SickLeave
                    && day.Type != DayType.ReligiousDay
                    && day.Type != DayType.PublicHoliday
                    && day.Type != DayType.OtherAbsence
                    && day.Type != DayType.BusinessAbsence
                    )
            {
                errors.Add("Invalid day type.");
            }

            if (day.Hours > 12)
            {
                errors.Add("Can't work more than 12 hours.");
            }

            return errors;
        }

        public static List<string> Validate(this Employee employee)
        {
            var errors = new List<string>();

            if (employee.StatusEmployee != StatusEmployee.Active
                    && employee.StatusEmployee != StatusEmployee.Leaver
                    && employee.StatusEmployee != StatusEmployee.Trial)
            {
                errors.Add("Employee can be active, leaver or trial.");
            }

            if (employee.Salary < 410 || employee.Salary > 10000)
            {
                errors.Add("Minimum salary can be 410 and maximum 10000.");
            }

            if (employee.FirstName.Length < 2)
            {
                errors.Add("First Name must be longer than 1 character.");
            }

            if (employee.LastName.Length < 2)
            {
                errors.Add("Last Name must be longer than 1 character.");
            }

            var regexPhone = new Regex("^[0-9+() ]*$");

            if (!regexPhone.IsMatch(employee.Phone))
            {
                errors.Add("Phone can't have letters.");
            }

            EmailValidation email = new EmailValidation();

            if (email.IsValidEmail(employee.Email) == false)
            {
                errors.Add("Wrong email.");
            }

            var regexName = new Regex("^[a-zA-ZšđčćžŠĐČĆŽ ]*$");

            if (!regexName.IsMatch(employee.FirstName))
            {
                errors.Add("First Name can't contain special characters and numbers.");
            }

            if (!regexName.IsMatch(employee.LastName))
            {
                errors.Add("Last Name can't contain special characters.");
            }

            if (employee.BeginDate > employee.EndDate)
            {
                errors.Add("End date must be after begin date.");
            }

            var years = DateTime.Now.Year - employee.BirthDate.Year;

            if (years < 18)
            {
                errors.Add("Emplyoee must be older than 18.");
            }

            return errors;
        }

        public static List<string> Validate(this Engagement engagement)
        {
            var errors = new List<string>();

            /*if (engagement.Hours > 40)
            {
                errors.Add("Can't work more than 12 hours.");
            }*/

            return errors;
        }

        public static List<string> Validate(this Project project)
        {
            var errors = new List<string>();

            if (project.StatusProject != StatusProject.Canceled
                    && project.StatusProject != StatusProject.Finished
                    && project.StatusProject != StatusProject.InProgress
                    && project.StatusProject != StatusProject.OnHold)
            {
                errors.Add("Invalid project status.");
            }

            if (project.Pricing != Pricing.FixedRate
                    && project.Pricing != Pricing.HourlyRate
                    && project.Pricing != Pricing.NotBillable
                    && project.Pricing != Pricing.PerCapitaRate)
            {
                errors.Add("Invalid pricing type.");
            }

            if (project.Name.Length < 2)
            {
                errors.Add("Project Name must be longer than 1 character.");
            }

            if (project.BeginDate > project.EndDate)
            {
                errors.Add("End date must be after begin date.");
            }

            return errors;
        }

        public static List<string> Validate(this Role role)
        {
            var errors = new List<string>();

            if (role.Type != RoleType.AppRole
                    && role.Type != RoleType.JobTitle
                    && role.Type != RoleType.TeamRole)
            {
                errors.Add("Invalid role status.");
            }

            if (role.Name.Length < 2)
            {
                errors.Add("Role Name must be longer than 1 character.");
            }

            if (role.Hrate > 100)
            {
                errors.Add("Hourly rate can't be higher than 100.");
            }

            if (role.Mrate > 10000)
            {
                errors.Add("Monthly rate can't be higher than 10000.");
            }

            return errors;
        }

        public static List<string> Validate(this Task task)
        {
            var errors = new List<string>();

            if (task.Hours > 40)
            {
                errors.Add("Hours spent on task can't be more than 40h.");
            }

            return errors;
        }

        public static List<string> Validate(this Team team)
        {
            var errors = new List<string>();

            //if (team.Name.Length < 2 )
            //{
            //    errors.Add("Team Name must be longer than 1 character.");
            //}

            //var regexName = new Regex("^[a-zA-ZšđčćžŠĐČĆŽ0-9 ]*$");

            //if (!regexName.IsMatch(team.Name))
            //{
            //    errors.Add("Team Name can't contain special characters.");
            //}

            //if (team.Projects.Count > 1)
            //{
            //    errors.Add("One team can have only one project.");
            //}

            return errors;
        }
    }
}