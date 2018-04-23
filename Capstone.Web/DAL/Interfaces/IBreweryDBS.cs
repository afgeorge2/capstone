using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL.Interfaces
{
    interface IBreweryDBS
    {
        bool AddNewBrewery(string breweryName);
        bool AddBrewer(string username, string password, bool isBrewer, int breweryID, string email);
    }
}
