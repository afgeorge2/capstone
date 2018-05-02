using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class BeerPhoto
    {
        public int BeerPhotoID { get; set; }
        public string Filename { get; set; }
        public int BeerID { get; set; }
        public int BreweryID { get; set; }



    }
}