using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Dapper;
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



        public int AddNewBrewery(string breweryName)
        {
            string sql = "INSERT INTO breweries (name) VALUES (@brewname)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@brewname", breweryName);
                int brewID = (int)cmd.ExecuteScalar();

                return brewID;
            }

        }


        public bool LinkBrewerToBrewery(int userID, int breweryID)
        {
            string sql = "UPDATE users SET is_brewer = 1, brewery_id = @breweryID WHERE id = @userID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@breweryID", breweryID);
                cmd.ExecuteNonQuery();

            }
            return true;

        }


        public bool AddNewBrewer(string username, string password, bool isBrewer, int breweryID, string email)
        {

            

            string sql = "INSERT INTO users values (@email, @username, @password, @isBrewer, @breweryID, @admin)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@isBrewer", isBrewer);
                cmd.Parameters.AddWithValue("@breweryID", breweryID);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@admin", false);
                int brewID = (int)cmd.ExecuteScalar();

            }
            return true;

        }




        public List<Brewery> GetAllBrewerys()
        {
            string sql = "Select * from breweries";
            List<Brewery> brews = new List<Brewery>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    brews.Add(GetBrewery(reader));
                }

            }
            return brews;

        }

        public Brewery GetBreweryByID(int brewID)
        {
            string sql = "SELECT * FROM breweries WHERE id=@id";
            Brewery brews = new Brewery();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@id", brewID);
                var reader = cmd.ExecuteReader();
                brews = GetBrewery(reader);

            }
            return brews;
        }



        public void UpdateBreweryInfo(Brewery b)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    int rows = conn.Execute(@"UPDATE breweries SET history=@history, address=@address, contact_name=@cname, contact_email=@email ,contact_phone=@phone WHERE breweries.id=@brewid;",
                       new { history = b.History, address = b.Address, cname = b.ContactName, email = b.ContactEmail, phone = b.ContactPhone, brewid = b.BreweryID, });
                }
            }
            catch
            {
                int a = 0;
            }

        }






        private Brewery GetBrewery(SqlDataReader reader)
        {//Be certain to check that the names read by the reader correlate with the column names in SQL!!**************************************************************************************
            Brewery thisUser = new Brewery()
            {
                BreweryName = Convert.ToString(reader["name"]),
                BreweryID = Convert.ToInt32(reader["id"])
            };
            return thisUser;
        }

    }



    //INSERT INTO breweries (name, history, address, contact_name, contact_email,contact_phone) VALUES(@name, @history, @address, @contact_name, @contact_email, @contact_phone);
}