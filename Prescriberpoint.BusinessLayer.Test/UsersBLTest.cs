using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Prescriberpoint.BusinessObjects;


namespace Prescriberpoint.BusinessLayer.Test
{
    [TestClass]
    public class UsersBLTest
    {

        UsersBL userBL;

        public UsersBLTest()
        {
            userBL = new UsersBL();
        }

        //This workaround method was created since VS does not create in order
        [TestMethod]
        public void TestAllInOrder()
        {
            AddUserAddAdmin();
            AddUserNotAdmin();
        }

        
        public void AddUserAddAdmin()
        {
            var user1 = new User()
            {
                Firstnane = "FirstName1",
                Lastnane = "LastName1",
                IsAdmin = true
                
            };
            var user2 = new User()
            {
                Firstnane = "FirstName1",
                Lastnane = "LastName1",
                IsAdmin = true
            };
            var userJson = JsonConvert.SerializeObject(user1);

            userBL.AddUser(user1);
            userBL.AddUser(user2);
            var userList = userBL.GetUsers();
            Assert.AreEqual(userList.Count, 0);
        }

        //Not valid to add user that are not admin
        public void AddUserNotAdmin()
        {
            var user1 = new User()
            {
                Firstnane = "FirstName1",
                Lastnane = "LastName1",
                IsAdmin = false

            };
            var user2 = new User()
            {
                Firstnane = "FirstName1",
                Lastnane = "LastName1",
                IsAdmin = false
            };

            userBL.AddUser(user1);
            userBL.AddUser(user2);
            var userList = userBL.GetUsers();
            userBL.DeleteUser(userList[0].UserId);
            userBL.DeleteUser(userList[1].UserId);
            userList = userBL.GetUsers();
            Assert.AreEqual(userList.Count, 0);
        }
    }
}
