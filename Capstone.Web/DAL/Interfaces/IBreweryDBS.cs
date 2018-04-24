using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL.Interfaces
{
    public interface IBreweryDBS
    {
        int AddNewBrewery(string breweryName);
        bool AddNewBrewer(string username, string password, bool isBrewer, int breweryID, string email);
        bool LinkBrewerToBrewery(int userID, int breweryID);
        List<Brewery> GetAllBrewerys();
        bool AddNewBeer(Beer newBeer);
        Brewery GetBreweryByID(int brewID);
    }
}
