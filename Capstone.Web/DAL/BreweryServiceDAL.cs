using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class BreweryServiceDAL : IBreweryServiceDAL
    {

        #region --- Contructors ---

        private const string _getLastIdSQL = " SELECT CAST(SCOPE_IDENTITY() as int);";

        private string connectionString;

        public BreweryServiceDAL()
        {
        }

        public BreweryServiceDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #endregion



        #region --- User Methods ---

        public User GetUser(string email)
        {
            User thisUser = new User();

            string sqlGetOne = "Select * from users WHERE users.email = @email;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlGetOne, conn);
                cmd.Parameters.AddWithValue("@email", email);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    thisUser = MapUserFromReader(reader);
                }
                return thisUser;
            }
            

        }

        //This varia

        public bool UserRegistration(User user)
        {
            bool IsSuccessful = false;
            string sqlregistration = @"Insert into users(username, password, email) Values(@userName, @passWord, @email)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlregistration, conn);
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@passWord", user.Password);
                cmd.Parameters.AddWithValue("@email", user.EmailAddress);


                IsSuccessful = (cmd.ExecuteNonQuery() > 0);
            }

            return IsSuccessful;
        }




        #endregion



        #region --- Brewery Methods ---

        public int AddNewBrewery(string breweryName)
        {
            string sql = "INSERT INTO breweries (name) VALUES (@brewname)";
            using (SqlConnection conn = new SqlConnection(connectionString))
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
            using (SqlConnection conn = new SqlConnection(connectionString))
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
            using (SqlConnection conn = new SqlConnection(connectionString))
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

            using (SqlConnection conn = new SqlConnection(connectionString))
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

            using (SqlConnection conn = new SqlConnection(connectionString))
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
            string sql = @"UPDATE breweries SET history=@history, address=@address, contact_name=@cname, contact_email=@email ,contact_phone=@phone WHERE breweries.id=@brewid";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@history", b.History);
                cmd.Parameters.AddWithValue("@address", b.Address);
                cmd.Parameters.AddWithValue("@cname", b.ContactName);
                cmd.Parameters.AddWithValue("@email", b.ContactEmail);
                cmd.Parameters.AddWithValue("@phone", b.ContactPhone);
                cmd.Parameters.AddWithValue("@brewid", b.BreweryID);

            }
        }

        public bool AddNewBeer(Beer newBeer)
        {
            //add image later
            string SQL_AddBeer = "Insert into beers (name, description, abv, beer_type, brewery_id) Values(@Name, @Description, @AlcoholByVolume, @BeerType, @brewId);";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_AddBeer, conn);
                cmd.Parameters.Add(new SqlParameter("@Name", newBeer.Name));
                cmd.Parameters.Add(new SqlParameter("@Description", newBeer.Description));
                //cmd.Parameters.Add(new SqlParameter("@image", newBeer.Image));
                cmd.Parameters.Add(new SqlParameter("@AlcoholByVolume", newBeer.AlcoholByVolume));
                cmd.Parameters.Add(new SqlParameter("@BeerType", newBeer.BeerType));
                cmd.Parameters.Add(new SqlParameter("@brewId", newBeer.BreweryId));
                cmd.ExecuteNonQuery();

            }

            return true;
        }




        #endregion


        #region --- Beer Methods ---


        public bool AddBeerReview()
        {
            throw new NotImplementedException();
        }


        #endregion













        #region --- SQL Readers ---

        private User MapUserFromReader(SqlDataReader reader)
        {
            User thisUser = new User()
            {
                EmailAddress = Convert.ToString(reader["email"]),
                UserName = Convert.ToString(reader["username"]),
                Password = Convert.ToString(reader["password"]),
                IsBrewer = Convert.ToBoolean(reader["is_brewer"]),
                IsAdmin = Convert.ToBoolean(reader["is_admin"])
            };
            var nullCheck = (reader["brewery_id"]);

            if (nullCheck != DBNull.Value)
            {
                thisUser.BreweryId = Convert.ToInt32(reader["brewery_id"]);
            }
            else
            {
                thisUser.BreweryId = 0;
            }

            return thisUser;
        }




    private Brewery GetBrewery(SqlDataReader reader)
        {
            Brewery brewery = new Brewery()
            {
                BreweryName = Convert.ToString(reader["name"]),
                BreweryID = Convert.ToInt32(reader["id"])
            };
            return brewery;
        }





        #endregion

    }
}