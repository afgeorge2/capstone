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
        public string History { get; set; }
        public string Imagery { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }







    }


 

}