using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IBreweryServiceDAL
    {

        //-----------------------------------------------------------User Methods
        bool UserRegistration(User user);
        User GetUser(string username, string password);
        User GetUser(string email);


        //-------------------------------------------------------Brewery Methods
        List<Brewery> GetAllBrewerys();
        int AddNewBrewery(string breweryName);
        bool AddNewBrewer(string username, string password, bool isBrewer, int breweryID, string email);
        Brewery GetBreweryByID(int brewID);
        void UpdateBreweryInfo(Brewery b);
        bool LinkBrewerToBrewery(int userID, int breweryID);

        //------------------------------------------------------Beer Methods

        bool AddNewBeer(Beer beer);


        //-------------------------------------------------------Review Methods

        bool AddBeerReview();
        







    }
}
