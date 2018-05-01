using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Web.Services.Description;

namespace Capstone.Web.Models.ViewModels
{
    public class IndexPageData
    {
        public List<Beer> GetAllTheBeers { get; set; }
        public List<Brewery> GetAllTheBreweries { get; set; }

        //This first set of properties holds the data for each brewery
         
        public string BreweryName { get; set; }
        public int BreweryID { get; set; }
        List<User> Users { get; set; }
        public string History { get; set; }
        public string Imagery { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        //This set of properties holds the data for each beer

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double AlcoholByVolume { get; set; }
        public string BeerType { get; set; }
        //public string BreweryName { get; set; }
        public int BreweryId { get; set; }
        public int ShowHide { get; set; }
    }

    //public enum Types
    //{
    //    Other,
    //    Ale,
    //    Lager,
    //    Malt,
    //    Porter,
    //    Stout,
    //    IPA
    //}
}
