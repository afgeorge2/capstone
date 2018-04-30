using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class BreweryAndBeersPhotoCombo
    {
        public List<BeerPhoto> BeerPic { get; set; }
        public List<BreweryPhoto> BreweryPic { get; set; }

    }
}