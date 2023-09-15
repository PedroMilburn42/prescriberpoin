using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Prescriberpoint.BusinessObjects;

namespace Prescriberpoint.DataLayer
{
    public class UsersDL 
    {
        public List<User> GetUsers()
        {
            List<User> listUsers = new List<User>();
            string connectString = ConfigurationManager.ConnectionStrings["PrescriberpointConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectString);
            con.Open();
            using (SqlCommand command = new SqlCommand("SELECT * FROM Users", con))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                               
                while (reader.Read())
                {
                    User user = new User();
                    user.UserId = reader.GetInt32(0);
                    user.Firstnane = reader.GetString(1);
                    user.Lastnane = reader.GetString(2);
                    user.IsAdmin = Convert.ToInt32(reader.GetString(3)) == 1;
                    listUsers.Add(user);
                }
               
            }
            return listUsers;
        }

        public User GetUser(int userId)
        {
            string connectString = ConfigurationManager.ConnectionStrings["PrescriberpointConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectString);
            con.Open();
            string query = string.Format("select * FROM Users where Userid = {0}", userId);
            using (SqlCommand command = new SqlCommand(query, con))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                User user = new User();
                while (reader.Read())
                {
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.Firstnane = reader["FirstName"].ToString();
                    user.Lastnane = reader["LastName"].ToString();
                    user.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                }
                return user;
            }
        }

        public bool DeleteUser(int userId)
        {
            string connectString = ConfigurationManager.ConnectionStrings["PrescriberpointConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectString);
            con.Open();
            string query = string.Format("delete FROM Users where Userid = {0}",userId);
            using (SqlCommand command = new SqlCommand(query, con))
            {
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected == 1;
            }
        }

        public bool UpdateUser(User user)
        {
            string connectString = ConfigurationManager.ConnectionStrings["PrescriberpointConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectString);
            con.Open();
            string query = string.Format("UPDATE Users(LastName, FirstName) VALUES({0}, {1}, {2}) WHERE UserId = {3})'", user.Lastnane, user.Firstnane, user.IsAdmin, user.UserId);
            using (SqlCommand command = new SqlCommand(query, con))
            {
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected == 1;
            }
        }

        public int AddUser(User user)
        {
            string connectString = ConfigurationManager.ConnectionStrings["PrescriberpointConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectString);
            con.Open();
            string query = string.Format("INSERT INTO Users(LastName, FirstName, IsAdmin) VALUES('{0}', '{1}', {2})", user.Lastnane, user.Firstnane, Convert.ToInt32(user.IsAdmin));
            using (SqlCommand command = new SqlCommand(query, con))
            {
                int lastId = command.ExecuteNonQuery();
                return lastId;
            }
        }
    }
}