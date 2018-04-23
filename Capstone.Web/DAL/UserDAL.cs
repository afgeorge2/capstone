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
    {//88888888888888888888888888888 BE SURE TO ADD THE NAMES OF THESE METHODS TO THE INTERFACE IUSERDAL***********************************************************************
        private string connectionString;

        public SurveyDAL(string connectionString)
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
                cmd.Parameters.AddWithValue("@userName", username);
                cmd.Parameters.AddWithValue("@passWord", password);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    thisUser = MapUserFromReader(reader);
                }            
            }

            return thisUser;
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