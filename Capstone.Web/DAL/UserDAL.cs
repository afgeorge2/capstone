using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;

namespace Capstone.Web
{
    public class UserDAL : IUserDAL
    {
        private string connectionString;

        public UserDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User GetUser(string username, string password)
        {
            User thisUser = new User();

            string sqlGetOne = "Select * from users WHERE users.username = @userName AND users.password = @passWord;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlGetOne, conn);
            }



            return thisUser;
        }

        
            public bool Registration(User user)
            {
                bool IsSuccessful = false;
                const string registration = @"Insert into user_info(username, password, email_address) Values('@username', ' @password', '@email_address')";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(registration, conn);
                    cmd.Parameters.AddWithValue("@username", user.UserName);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@email_address", user.EmailAddress);


                    IsSuccessful = (cmd.ExecuteNonQuery() > 0);
                }

                return IsSuccessful;
            }

            private User MapUserFromReader(SqlDataReader reader)
        {//Be certain to check that the names read by the reader correlate with the column names in SQL!!**************************************************************************************
            User thisUser = new User()
            {
                UserName = Convert.ToString(reader["username"]),
                Password = Convert.ToString(reader["password"]),
                IsBrewer = Convert.ToBoolean(reader["is_brewer"]),
                IsAdmin = Convert.ToBoolean(reader["is_admin"]),
                EmailAddress = Convert.ToString(reader["email_address"]),
                BreweryId = Convert.ToInt32(reader["brewery_id"])
            };
            return thisUser;
        }
    }
}