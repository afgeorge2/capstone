using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class HoursViewModel
    {
        public int BrewID { get; set; }
        public List<DaysHoursOperation> DaysHours { get; set; }



    }
}

