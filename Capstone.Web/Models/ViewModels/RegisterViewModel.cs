using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "required")]
        public string UserName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "required")]
        [RegularExpression(@"^(?=.+\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
        public string Password { get; set; }

        [Required(ErrorMessage = "required")]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
} 