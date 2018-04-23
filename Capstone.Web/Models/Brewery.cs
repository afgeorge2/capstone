using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{




    public class Brewery
    {
        public string BreweryName { get; set; }
        public int BreweryID { get; set; }
        List<User> Users { get;set; }








    }


    public class TestDB
    {
        private const string _getLastIdSQL = " SELECT CAST(SCOPE_IDENTITY() as int);";
        string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=GroceryList;Integrated Security=True";

        public TestDB()
        {

        }

        public TestDB(string connectionString)
        {
            _connectionString = connectionString;
        }



        public bool AddNewBrewery(string breweryName, int userID)
        {
            string sql = "INSERT INTO Brewery_Info VALUES (@brewname)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql+ _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@brewname", breweryName);
                int brewID = (int)cmd.ExecuteScalar();



            }



                return true;

        }

    }


}