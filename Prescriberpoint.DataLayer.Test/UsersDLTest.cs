using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prescriberpoint.BusinessObjects;

namespace Prescriberpoint.DataLayer.Test
{
    [TestClass]
    public class UsersDLTest
    {
        UsersDL userDL;
        int currentUser1Id = 0;
        int currentUser2Id = 0;

        public UsersDLTest()
        {
            userDL = new UsersDL();
        }

        //This workaround method was created since VS does not create in order
        [TestMethod]
        public void TestAllInOrder()
        {
            GetUsersWithEmptyList();
            GetUseWrongId();
            Add2Users();
            GetExistingUser();
            DeleteExistingUser();
            GetExistingUser2();
            DeleteExistingUser2();
            VeryUserTableIsEmpty();
        }

        
        public void GetUsersWithEmptyList()
        {
            var userList = userDL.GetUsers();
            Assert.AreEqual(userList.Count, 0);
        }

        
        public void GetUseWrongId()
        {
            var user = userDL.GetUser(int.MaxValue);
            Assert.AreEqual(user.UserId, 0);
        }

        
        public void Add2Users()
        {
            var user1 = new User()
            {
                Firstnane = "FirstName1",
                Lastnane = "LastName1",
                IsAdmin = true
            };
            var user2 = new User()
            {
                Firstnane = "FirstName2",
                Lastnane = "LastName2",
                IsAdmin = true
            };
            userDL.AddUser(user1);
            userDL.AddUser(user2);
            var userList = userDL.GetUsers();
            Assert.AreEqual(userList.Count, 2);
            currentUser1Id = userList[0].UserId;
            currentUser2Id = userList[1].UserId;
        }

        //Get the previous created user
        
        public void GetExistingUser()
        {
            var user= userDL.GetUser(currentUser1Id);
            Assert.AreEqual(user.UserId, currentUser1Id);
        }
        //Delete the previous user
        
        public void DeleteExistingUser()
        {
            userDL.DeleteUser(currentUser1Id);
            var user = userDL.GetUser(currentUser1Id);
            Assert.AreEqual(user.UserId, 0);
        }

        //Verify the existing user still exists
        
        public void GetExistingUser2()
        {
            var user = userDL.GetUser(currentUser2Id);
            Assert.AreEqual(user.UserId, currentUser2Id);
        }
        //Delete the previous user 2
        
        public void DeleteExistingUser2()
        {
            userDL.DeleteUser(currentUser2Id);
            var user = userDL.GetUser(currentUser2Id);
            Assert.AreEqual(user.UserId, 0);
        }

        //Verify the whole user table is empty
        
        public void VeryUserTableIsEmpty()
        {
            var userList = userDL.GetUsers();
            Assert.AreEqual(userList.Count, 0);
        }

    }
}
