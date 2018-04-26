using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class AddBeerModel
    {
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        public string Description { get; set; }
        
        //public string Image { get; set; }

        [Required(ErrorMessage = "required")]
        public double AlcoholByVolume { get; set; }

        [Required(ErrorMessage = "required")]
        public Types BeerType { get; set; }

        public int BreweryId { get; set; }
    }

    public enum Types
    {
        Other,
        Ale,
        Lager,
        Malt,
        Porter,
        Stout,
        IPA
    }
}
