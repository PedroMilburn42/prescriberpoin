using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Prescriberpoint.WebApi.Controllers;
using Prescriberpoint.BusinessObjects;

namespace Prescriberpoint.WebApi.Tests.Controllers
{
    
    // This tests assune the user table is empty
    [TestClass]
    
    public class UserControllerTest
    {
        const string SystemKey = "54622F7D-8A9D-4CC3-8A0D-ADD1DBAB07C9";
        UserController controller;
        int currentUser1Id = 0;
        int currentUser2Id = 0;

        public UserControllerTest()
        {
            controller = new UserController();
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
            var userJsonList = controller.GetUsers();
            var userList = JsonConvert.DeserializeObject<List<User>>(userJsonList);
            Assert.AreEqual(userList.Count, 0);
        }

        
        public void GetUseWrongId()
        {
            var userJson = controller.Get(int.MaxValue);
            Assert.AreEqual(userJson, string.Empty);
        }

        
        public void Add2Users()
        {
            var user1 = new User()
            {
                Firstnane = "FirstName1",
                Lastnane = "LastName1",
                IsAdmin = false
            };
            var user2 = new User()
            {
                Firstnane = "FirstName2",
                Lastnane = "LastName2" ,
                IsAdmin = false
            };
            var userJson = JsonConvert.SerializeObject(user1);
            controller.Post(userJson);
            userJson = JsonConvert.SerializeObject(user2);
            controller.Post(userJson);
            var userJsonList = controller.GetUsers();
            var userList = JsonConvert.DeserializeObject<List<User>>(userJsonList);
            Assert.AreEqual(userList.Count, 2);
            currentUser1Id = userList[0].UserId;
            currentUser2Id = userList[1].UserId;

        }

        //Get the previous created user        
        public void GetExistingUser()
        {
            var userJson = controller.Get(currentUser1Id);
            var userDB = JsonConvert.DeserializeObject<User>(userJson);
            Assert.AreEqual(userDB.UserId, currentUser1Id);
        }

        //Delete the previous user
        public void DeleteExistingUser()
        {
            controller.Delete(currentUser1Id);
            var userJson = controller.Get(currentUser1Id);          
            Assert.AreEqual(userJson,string.Empty);
        }

        //Verify the existing user still exists
        public void GetExistingUser2()
        {
            var userJson = controller.Get(currentUser2Id);
            var userDB = JsonConvert.DeserializeObject<User>(userJson);
            Assert.AreEqual(userDB.UserId, currentUser2Id);
        }
        //Delete the previous user 2
        public void DeleteExistingUser2()
        {
            controller.Delete(currentUser2Id);
            var userJson = controller.Get(currentUser2Id);
            Assert.AreEqual(userJson, string.Empty);
        }

        //Verify the whole user table is empty
        public void VeryUserTableIsEmpty()
        {
            var userJsonList = controller.GetUsers();
            var userList = JsonConvert.DeserializeObject<List<User>>(userJsonList);
            Assert.AreEqual(userList.Count, 0);
        }

    }
}
