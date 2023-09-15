using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prescriberpoint.BusinessObjects;

namespace Prescriberpoint.DataLayer
{
    public interface IUsers : IDisposable
    {
        List<User> GetUsers();
        User GetUser(int UserId);
        bool DeleteUser(int UserId);
        bool UpdateUser(User user);
        int AddUser(User user);
    }

}