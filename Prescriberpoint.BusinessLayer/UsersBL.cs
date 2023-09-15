using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prescriberpoint.BusinessObjects;
using Prescriberpoint.DataLayer;

namespace Prescriberpoint.BusinessLayer
{
    public class UsersBL
    {
        UsersDL dataLayer;
        public UsersBL()
        {
            dataLayer = new UsersDL();
        }

        public List<User> GetUsers()
        {
            return dataLayer.GetUsers();
        }

        public User GetUser(int userId)
        {
            return dataLayer.GetUser(userId);
        }

        public bool DeleteUser(int userId)
        {
            var user = GetUser(userId);
            if (!user.IsAdmin)
            {
                return dataLayer.DeleteUser(userId);
            }
            return false;
        }

        public bool UpdateUser(User user)
        {
            if (!user.IsAdmin)
            {
                return dataLayer.UpdateUser(user);
            }
            return false;

        }

        public int AddUser(User user)
        {
            if (!user.IsAdmin)
            {
                return dataLayer.AddUser(user);
            }
            return 0;
        }        
    }
}
