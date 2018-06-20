using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;

namespace TimeKeeperTest
{
        [TestClass]
        public class DeleteEmpTest
        {
            [TestMethod]
            public void CheckIfDeletedEmp()
            {
                TimeKeeperContext context = new TimeKeeperContext();

                Employee emp = new Employee()
                {
                    FirstName = "Bekir",
                    LastName = "Bukvarevic",
                    Position = context.Roles.Find("DEV")
                };

                //Act
                context.Employees.Add(emp);
                context.SaveChanges();
               
                context.Employees.Remove(emp);
                context.SaveChanges();
                
                //Assert
                emp = context.Employees
                      .FirstOrDefault(x => x.FirstName == "Bekir");
                Assert.IsNull(emp);
            }
        }
    
}
