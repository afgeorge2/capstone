﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Beer
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double AlcoholByVolume { get; set; }
        public string BeerType { get; set; }
        public string BreweryName { get; set; }
        public int BreweryId { get; set; }
    }
}