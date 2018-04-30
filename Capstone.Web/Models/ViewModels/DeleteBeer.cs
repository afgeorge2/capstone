using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class DeleteBeer
    {
        public int BreweryId { get; set; }
        public string Name { get; set; }
        public List<Beer> DropDownBeers{get; set;}
    }
}