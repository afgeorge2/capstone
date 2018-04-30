﻿using Capstone.Web.Models;
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




        public User SearchUserToAddBrewery(string email)
        {


            User users = new User();

            string sql = @"SELECT * FROM users WHERE email = @email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@email", email);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users = MapUserFromReader(reader);
                }
            }
            return users;
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

        public void UpdateUserBrewer(int brewID, string email)
        {

            string sql = @"UPDATE users SET is_brewer = @isbrewer, brewery_id = @brewID WHERE email = @email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@isbrewer", true);
                cmd.Parameters.AddWithValue("@brewID", brewID);
                cmd.Parameters.AddWithValue("@email", email);

                var reader = cmd.ExecuteReader();

            }
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
                while (reader.Read())
                {
                    brews = GetBrewery(reader);
                }

            }
            return brews;
        }

        public void UpdateBreweryInfo(Brewery b)
        {
            string sql = @"UPDATE breweries SET history=@history, address=@address, contact_name=@cname, contact_email=@email ,contact_phone=@phone WHERE breweries.id=@brewid";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@history", b.History);
                cmd.Parameters.AddWithValue("@address", b.Address);
                cmd.Parameters.AddWithValue("@cname", b.ContactName);
                cmd.Parameters.AddWithValue("@email", b.ContactEmail);
                cmd.Parameters.AddWithValue("@phone", b.ContactPhone);
                cmd.Parameters.AddWithValue("@brewid", b.BreweryID);
                cmd.ExecuteNonQuery();

            }
        }


        public void UpdateBreweryHours(HoursViewModel m)
        {
            m.DaysHours[0].DayOfWeek = "Monday";
            m.DaysHours[1].DayOfWeek = "Tuesday";
            m.DaysHours[2].DayOfWeek = "Wednesday";
            m.DaysHours[3].DayOfWeek = "Thursday";
            m.DaysHours[4].DayOfWeek = "Friday";
            m.DaysHours[5].DayOfWeek = "Saturday";
            m.DaysHours[6].DayOfWeek = "Sunday";

            string removeOLD = @"delete from operation where brewery_id = @brewID";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(removeOLD, conn);
                cmd.Parameters.AddWithValue("@brewID", m.BrewID);
                cmd.ExecuteNonQuery();
            }

            foreach (var day in m.DaysHours)
            {
                string sql = @"INSERT INTO OPERATION VALUES (@brewID, @day, @open, @close);";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                    cmd.Parameters.AddWithValue("@brewID", m.BrewID);
                    cmd.Parameters.AddWithValue("@day", day.DayOfWeek);
                    cmd.Parameters.AddWithValue("@open", day.Opens);
                    cmd.Parameters.AddWithValue("@close", day.Closes);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public List<DaysHoursOperation> GetHoursForBrewery(int brewID)
        {
            List<DaysHoursOperation> hours = new List<DaysHoursOperation>();
            string sql = "SELECT * FROM OPERATION WHERE brewery_id = 1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    hours.Add(GetOps(reader));
                }
            }
            return hours;
        }



        public string AddBreweryPhoto(string filepath, int? brewID)
        {

            string sql = "UPDATE breweries SET imagery = @filepath WHERE id = @brewID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@filepath", filepath);
                cmd.Parameters.AddWithValue("@brewID", brewID);
                cmd.ExecuteReader();

            }



            return filepath;

        }

        #endregion


        #region --- Beer Methods ---


        public bool AddNewBeer(AddBeerModel newBeer)
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

        //get beers from DB for dropdown in showhide
        public List<Beer> GetAllBeersFromBrewery(int breweryId)
        {
            string SQL_Beers = "Select * from beers where brewery_id = @breweryId";
            List<Beer> shb = new List<Beer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_Beers, conn);
                cmd.Parameters.AddWithValue("@breweryId", breweryId);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    shb.Add(GetBeersShowHideFromReader(reader));
                }

                return shb;
            }
        }

        public List<Beer> GetAllBeers()
        {
            string SQL_Beers = "Select * from beers";
            List<Beer> beer = new List<Beer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_Beers, conn);


                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    beer.Add(GetBeerFromReader(reader));
                }

                return beer;
            }
        }

        public Beer GetBeersById(int beerId)
        {
            string sql = "Select * from beers where id= @id";
            Beer beer = new Beer();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@beerID", beerId);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    beer = (GetBeerFromReader(reader));
                }

            }
            return beer;
        }

        public bool AddBeerReview()
        {
            throw new NotImplementedException();
        }

        public void UpdateShowHide(List<Beer> beers)
        {
            string SQL_ShowHide = @"UPDATE beers SET show_hide=@showhide WHERE brewery_id=@brewid and name=@Name";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_ShowHide, conn);
                foreach (Beer b in beers)
                {
                    cmd.Parameters.AddWithValue("@showhide", b.ShowHide);
                    cmd.Parameters.AddWithValue("@brewery_id", b.BreweryId);
                    cmd.Parameters.AddWithValue("@name", b.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }





        #endregion


        #region --- SQL Readers ---

        public User MapUserFromReader(SqlDataReader reader)
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


        private DaysHoursOperation GetOps(SqlDataReader reader)
        {

            DaysHoursOperation hours = new DaysHoursOperation()
            {

                DayOfWeek = Convert.ToString(reader["day"]),
                Opens = Convert.ToString(reader["opens"]),
                Closes = Convert.ToString(reader["closes"])
            };

            return hours;
        }



        private Brewery GetBrewery(SqlDataReader reader)
        {
            Brewery brewery = new Brewery()
            {
                BreweryName = Convert.ToString(reader["name"]),
                Address = Convert.ToString(reader["address"]),
                ContactEmail = Convert.ToString(reader["contact_email"]),
                ContactName = Convert.ToString(reader["contact_name"]),
                ContactPhone = Convert.ToString(reader["contact_phone"]),
                History = Convert.ToString(reader["history"]),
                Imagery = Convert.ToString(reader["imagery"]),
                BreweryID = Convert.ToInt32(reader["id"])
            };
            return brewery;
        }

        private Beer GetBeerFromReader(SqlDataReader reader)
        {
            Beer beer = new Beer()
            {
                BreweryId = Convert.ToInt32(reader["id"]),
                Name = Convert.ToString(reader["name"]),
                Image =Convert.ToString(reader["image"]),
                Description = Convert.ToString(reader["description"]),
                
                


            };
            return beer;
        }

            private Beer GetBeersShowHideFromReader(SqlDataReader reader)
        {
            Beer beers = new Beer()
            {
                Name = Convert.ToString(reader["name"]),
                ShowHide = Convert.ToInt32(reader["show_hide"])
            };
            return beers;
        }

        bool IBreweryServiceDAL.AddBeerReview()
        {
            throw new NotImplementedException();
        }

        


        #endregion




    }
}
