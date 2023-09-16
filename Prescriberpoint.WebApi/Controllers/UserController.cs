using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Prescriberpoint.BusinessObjects;
using Prescriberpoint.BusinessLayer;
using System.Web;

namespace Prescriberpoint.WebApi.Controllers
{
    public class UserController : ApiController
    {
        const string SystemKey = "54622F7D-8A9D-4CC3-8A0D-ADD1DBAB07C9";
        UsersBL businessLayer;
        public UserController()
        {
            businessLayer = new UsersBL();
            var key = GetKeyFromHeader();
            if (key == null || key != SystemKey)
            {
                throw new UnauthorizedAccessException();
            }
        }

        // GET: api/User
        public string GetUsers()
        {
            var userList = businessLayer.GetUsers();
            var userJson = JsonConvert.SerializeObject(userList);
            return userJson;
        }

        // GET: api/User/5
        public string Get(int id)
        {

            
            var userDB = businessLayer.GetUser(id);
            if (userDB.UserId == 0)
            {
                return string.Empty;
            }
            var userJson = JsonConvert.SerializeObject(userDB);
            return userJson;
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
            var userAdd = JsonConvert.DeserializeObject<User>(value);
            businessLayer.AddUser(userAdd);
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
            var userUpdated = JsonConvert.DeserializeObject<User>(value);
            userUpdated.UserId = id;
            businessLayer.UpdateUser(userUpdated);
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
            businessLayer.DeleteUser(id);
        }

        public static string GetKeyFromHeader()
        {
            if (HttpContext.Current == null) // It is running tests. Don't check security
            {
                return SystemKey;
            }
            var headers = HttpContext.Current.Request.Headers;
             var key = headers.AllKeys.FirstOrDefault(k => k == "key" );
            if (key != null)
            {
                return headers["key"];
            }
            return null;
        }
    }
}
