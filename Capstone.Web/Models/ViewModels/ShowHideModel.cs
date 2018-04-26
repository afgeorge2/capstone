using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class ShowHideModel
    {
        public int BreweryId { get; set; }
        public List<ShowHideBeer> ShowHide { get; set; }
    }
}