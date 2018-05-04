using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "REQUIRED")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        //[MinLength(8, ErrorMessage = "Must be at least 8 characters")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Must contain at least 1 uppercase letter, 1 lowercase letter, and 1 number\nMust be at least 8 characters (can contain special characters)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [EmailAddress(ErrorMessage ="Not a valid Email")]
        public string EmailAddress { get; set; }

        public int UserId { get; set; }

    }
} 