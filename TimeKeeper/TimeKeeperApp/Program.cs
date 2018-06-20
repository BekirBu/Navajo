using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Repositories;

namespace TimeKeeperApp
{
    class Program
    {

        public class Result
        {
            public Employee Employee { get; set; }
            public decimal TotalHours { get; set; }
            public decimal AverageHours { get; set; }
        }



        static void Main(string[] args)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {

                // PROJECTS AND TEAMS 
                //var query = unit.Projects.Get().ToList();
                //Console.WriteLine("Teams and Projects: ");
                //Console.WriteLine();

                //foreach (var project in query)
                //{
                //    Console.Write($"{project.Team.Name} : ");
                //    Console.WriteLine($"{project.Name} ");
                //}


                // ENTER TEAM AND DISPLAY MEMBERS
                //Console.WriteLine("Enter Team name: ");
                //string teamName = Console.ReadLine();

                //var teams = unit.Teams.Get().Select(x => x.Name).ToList();
                //if (teams.Contains(teamName))
                //{
                //    var query = unit.Employees.Get()
                //           .SelectMany(t => t.Engagement)
                //           .Where(e => e.Team.Name == teamName)
                //           .GroupBy(x => x.Employee, y => y.Hours)
                //           .Select(w => new
                //           {
                //               w.Key.FirstName,
                //               w.Key.LastName
                //           })
                //           .ToList();


                //    Console.WriteLine();
                //    Console.WriteLine("Team members: ");
                //    Console.WriteLine("-----------------------------------");

                //    foreach (var emp in query)
                //    {
                //        Console.WriteLine($"{emp.FirstName} {emp.LastName} ");
                //        Console.WriteLine();
                //    }
                //}


                // ENTER YEAR AND CHEKCK WHO EXCEEDED NUMBER OF VACATION DAYS
                //Console.WriteLine("Enter Year: ");
                //int year = int.Parse(Console.ReadLine());

                //var query = unit.Employees.Get()
                //           .SelectMany(d => d.Days)
                //           .Where(x => x.Date.Year == year && x.Type == DayType.Vacation)
                //           .GroupBy(e => e.Employee)
                //           .Select(w => new
                //           {
                //               w.Key,
                //               vd = w.Count()
                //           })
                //           .ToList();

                //Console.WriteLine();
                //Console.WriteLine("Employers that exceeded allowed number of vacation days: ");

                //foreach (var emp in query)
                //{
                //    if (emp.vd > 18)
                //    {
                //        Console.WriteLine($"{emp.Key.FirstName} {emp.Key.LastName}: {emp.vd} ");
                //    }
                //}


                //NUMBER OF ROLES IN COMPANY
                Console.WriteLine("Count of all roles in company:");
                var query = unit.Employees.Get()
                           .SelectMany(e => e.Engagement)
                           .GroupBy(r => r.Role)
                           .Select(w => new
                           {
                               nameRole = w.Key.Name,
                               countRole = w.Count()
                           })
                           .ToList();

                Console.WriteLine();

                foreach (var emp in query)
                {
                    Console.WriteLine($"{emp.nameRole} {emp.countRole}");
                }

            }
        }
    }
}

