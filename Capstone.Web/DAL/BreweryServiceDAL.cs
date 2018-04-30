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

                brews.BreweryPhoto = GetBreweryPhoto(brewID);

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


        //string open0, string close0, string open1, string close1, string open2, string close2, string open3, string close3, string open4, string close4, string open5, string close5, string open6, string close6
        //public void UpdateBreweryHours(HoursViewModel m)


        public void UpdateBreweryHours(int brewID, string open0, string close0, string open1, string close1, string open2, string close2, string open3, string close3, string open4, string close4, string open5, string close5, string open6, string close6)
        {

            //DaysHoursOperation day = new DaysHoursOperation();

            HoursViewModel m = new HoursViewModel();
            m.DaysHours = new List<DaysHoursOperation>() {

                new DaysHoursOperation(){DayOfWeek = "Monday"},
                new DaysHoursOperation(){DayOfWeek = "Tuesday"},
                new DaysHoursOperation(){DayOfWeek = "Wednesday"},
                new DaysHoursOperation(){DayOfWeek = "Thursday"},
                new DaysHoursOperation(){DayOfWeek = "Friday"},
                new DaysHoursOperation(){DayOfWeek = "Saturday"},
                new DaysHoursOperation(){ DayOfWeek = "Sunday" }
            };
            m.BrewID = brewID;

            m.DaysHours[0].Opens = open0 ?? "CLOSED";
            m.DaysHours[1].Opens = open1 ?? "CLOSED";
            m.DaysHours[2].Opens = open2 ?? "CLOSED";
            m.DaysHours[3].Opens = open3 ?? "CLOSED";
            m.DaysHours[4].Opens = open4 ?? "CLOSED";
            m.DaysHours[5].Opens = open5 ?? "CLOSED";
            m.DaysHours[6].Opens = open6 ?? "CLOSED";

            m.DaysHours[0].Closes = close0 ?? "CLOSED";
            m.DaysHours[1].Closes = close1 ?? "CLOSED";
            m.DaysHours[2].Closes = close2 ?? "CLOSED";
            m.DaysHours[3].Closes = close3 ?? "CLOSED";
            m.DaysHours[4].Closes = close4 ?? "CLOSED";
            m.DaysHours[5].Closes = close5 ?? "CLOSED";
            m.DaysHours[6].Closes = close6 ?? "CLOSED";



            foreach (var day in m.DaysHours)
            {
                if (day.Opens == null)
                {
                    day.Opens = "CLOSED";
                }
                if (day.Closes == null)
                {
                    day.Closes = "CLOSED";
                }
            }




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
            string sql = "SELECT * FROM OPERATION WHERE brewery_id = @brewid";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@brewid", brewID);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    hours.Add(GetOps(reader));
                }
            }

            //foreach (var day in hours)
            //{

            //    if (day.Opens == "Not Set")
            //    {
            //        day.Opens = "";
            //    }
            //    if (day.Closes == "Not Set")
            //    {
            //        day.Closes = "";
            //    }
            //}

            return hours;
        }



        #endregion





        #region --- Photos ---

        public void UploadBreweryPhoto(string filename, int brewID, bool profilePic)
        {

            string sql = @"INSERT INTO breweryPhotos VALUES( @FILE_NAME, @brewery_id, @profile_pic)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@FILE_NAME", filename);
                cmd.Parameters.AddWithValue("@brewery_id", brewID);
                cmd.Parameters.AddWithValue("@profile_pic", profilePic);
                cmd.ExecuteReader();

            }

        }

        public void UploadBeerPhoto(string filename, int beerID)
        {
            string sql = @"INSERT INTO beerPhotos VALUES( @FILE_NAME, @beer_id)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@FILE_NAME", filename);
                cmd.Parameters.AddWithValue("@beer_id", beerID);
                cmd.ExecuteReader();

            }

        }



        private BreweryPhoto GetBreweryPhoto(int brewID)
        {
            string sql = @"SELECT * FROM breweryPhotos WHERE brewery_id = @brewID";

            BreweryPhoto brewphoto = new BreweryPhoto();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@brewID", brewID);
                //cmd.ExecuteReader();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    brewphoto = (MakeBreweryPhoto(reader));
                }
            }
            return brewphoto;
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
            string SQL_Beers = "Select * from beers;";
            List<Beer> shb = new List<Beer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_Beers, conn);


                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    shb.Add(GetBeersShowHideFromReader(reader));
                }

                return shb;
            }
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

        private BeerPhoto MakeBeerPhoto(SqlDataReader reader)
        {
            BeerPhoto beerpic = new BeerPhoto()
            {
                Filename = Convert.ToString(reader["FILE_NAME"]),
                BeerID = Convert.ToInt32(reader["beer_id"])
            };
            return beerpic;
        }

        private BreweryPhoto MakeBreweryPhoto(SqlDataReader reader)
        {
            BreweryPhoto breweryPhoto = new BreweryPhoto()
            {
                Filename = Convert.ToString(reader["FILE_NAME"]),
                BreweryID = Convert.ToInt32(reader["brewery_id"]),
                ProfilePic = Convert.ToBoolean(reader["profile_pic"])
            };
            return breweryPhoto;
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
