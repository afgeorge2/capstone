using Capstone.Web.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class BreweryDAL : IBreweryDBS
    {

        private const string _getLastIdSQL = " SELECT CAST(SCOPE_IDENTITY() as int);";
        string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Brewery;Integrated Security=True";

        public BreweryDAL()
        {

        }

        public BreweryDAL(string connectionString)
        {
            _connectionString = connectionString;
        }



        public bool AddNewBrewery(string breweryName)
        {
            string sql = "INSERT INTO Brewery_Info VALUES (@brewname)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@brewname", breweryName);
                int brewID = (int)cmd.ExecuteScalar();

            }
            return true;

        }

        public bool AddBrewer(string username, string password, bool isBrewer, int breweryID, string email)
        {
            string sql = "INSERT INTO user_info (@username, @password, @isBrewer, @breweryID, @email)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@isBrewer", isBrewer);
                cmd.Parameters.AddWithValue("@breweryID", breweryID);
                cmd.Parameters.AddWithValue("@email", email);
                int brewID = (int)cmd.ExecuteScalar();

            }
            return true;

        }

    }
}