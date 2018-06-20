using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;

namespace TimeKeeperTest
{
    [TestClass]
    public class RoleTest
    {
        [TestMethod]
        public void CheckRoles()
        {
            //Arrange
            TimeKeeperContext context = new TimeKeeperContext();
            //Act
            int roles = context.Roles.Count();
            //Assert
            Assert.AreEqual(2, roles);
        }
    }
}
