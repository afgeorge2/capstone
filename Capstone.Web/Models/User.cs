using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class User
    {
        //These are the properties of each user
        [Required(ErrorMessage = "An email address is required")]
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Must enter a password")]
        public string Password { get; set; }
        public bool? IsBrewer { get; set; }
        public int? BreweryId { get; set; }
        public bool IsAdmin { get; set; }
        public bool UserId { get; set; }



    }
}