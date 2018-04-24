using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
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






        private Brewery GetBrewery(SqlDataReader reader)
        {//Be certain to check that the names read by the reader correlate with the column names in SQL!!**************************************************************************************
            Brewery thisUser = new Brewery()
            {
                BreweryName = Convert.ToString(reader["name"]),
                BreweryID = Convert.ToInt32(reader["id"])
            };
            return thisUser;
        }



        public bool AddNewBeer(Beer newBeer)
        {
            //add image later
            string SQL_AddSurvey = "Insert into beers (name, description, abv, beer_type, brewery_id) Values(@name, @description, @abv, @beertype, @breweryid);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_AddSurvey, conn);
                cmd.Parameters.Add(new SqlParameter("@name", newBeer.Name));
                cmd.Parameters.Add(new SqlParameter("@description", newBeer.Description));
                //cmd.Parameters.Add(new SqlParameter("@image", newBeer.Image));
                cmd.Parameters.Add(new SqlParameter("@abv", newBeer.AlcoholByVolume));
                cmd.Parameters.Add(new SqlParameter("@beertype", newBeer.BeerType));
                cmd.Parameters.Add(new SqlParameter("@breweryid", newBeer.BreweryId));
                cmd.ExecuteNonQuery();

            }

            return true;
        }

    }
}