using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Beer
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(2000)]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        [Range(0, 100)]
        public double AlcoholByVolume { get; set; }
        [Required]
        public string BeerType { get; set; }
        public string BreweryName { get; set; }
        public int BreweryId { get; set; }
    }
}