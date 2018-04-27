using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
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
      //  User GetUser(LoginViewModel model);
        User GetUser(string email);


        //-------------------------------------------------------Brewery Methods
        List<Brewery> GetAllBrewerys();
        int AddNewBrewery(string breweryName);
        bool AddNewBrewer(string username, string password, bool isBrewer, int breweryID, string email);
        Brewery GetBreweryByID(int brewID);
        void UpdateBreweryInfo(Brewery b);
        bool LinkBrewerToBrewery(int userID, int breweryID);
        void UpdateBreweryHours(HoursViewModel m);
        string AddBreweryPhoto(string filepath, int? brewID);
        //------------------------------------------------------Beer Methods

        bool AddNewBeer(AddBeerModel beer);
        List<Beer> GetAllBeersFromBrewery(int breweryId);
        List<Beer> GetAllBeers();


        //-------------------------------------------------------Review Methods

        bool AddBeerReview();
        







    }
}
