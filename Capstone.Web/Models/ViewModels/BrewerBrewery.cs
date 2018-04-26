using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.Viewmodel
{
    public class BrewerBrewery
    {

        [Required(ErrorMessage = "required")]
        [MinLength(3, ErrorMessage = "required")]
        public string BreweryName { get; set; }


        [Required]
        [MinLength(8, ErrorMessage = "required")]
        [RegularExpression(@"^(?=.+\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
        public string Password { get; set; }

 
        [Required]
        [MinLength(6, ErrorMessage = "required")]
        public string UserName { get; set; }


        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }


        [Required]
        [RegularExpression(@"\d+", ErrorMessage = "must select a brewery")]
        public int BreweryID { get; set; }
    }
}


