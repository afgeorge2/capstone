using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class BreweryPhoto
    {
        public string Filename { get; set; }
        public int BreweryID { get; set; }
        public bool ProfilePic { get; set; }

        //public Brewery Brewery { get; set; }


    }
}