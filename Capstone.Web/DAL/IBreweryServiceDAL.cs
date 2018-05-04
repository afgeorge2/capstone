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
        User LoginUser(string email);
        User GetUser(string email,string userName);
        User SearchUserToAddBrewery(string email);
        void UpdateUserBrewer(int brewID, string email);


        //-------------------------------------------------------Brewery Methods
        List<Brewery> GetAllBrewerys();
        int AddNewBrewery(string breweryName);
        bool AddNewBrewer(string username, string password, bool isBrewer, int breweryID, string email);
        Brewery GetBreweryByID(int brewID);
        void UpdateBreweryInfo(Brewery b);
        bool LinkBrewerToBrewery(int userID, int breweryID);

        //void UpdateBreweryHours(HoursViewModel m);
        void UpdateBreweryHours(int brewID, string open0, string close0, string open1, string close1, string open2, string close2, string open3, string close3, string open4, string close4, string open5, string close5, string open6, string close6);


        List<DaysHoursOperation> GetHoursForBrewery(int brewID);
        
        //------------------------------------------------------Beer Methods

        bool AddNewBeer(AddBeerModel beer, string photoname);
        void UpdateBeer(Beer b);
        List<Beer> GetAllBeersFromBrewery(int breweryId);
        List<Beer> GetAllBeers();
        void UpdateShowHide(int beerID, int showHide);
        bool DeleteBeer(DeleteBeer beer);
        Beer GetBeersById(int beerId);

        //-------------------------------------------------------Review Methods

        bool AddBeerReview(ReviewModel m);
        List<Review> GetBeerReviewsById(int beerId);


        //-------------------------------------------------------Photo Methods

        void UploadBreweryPhoto(string filename, int brewID, bool profilePic);
        void UploadBeerPhoto(string filename, int beerID);
        //BreweryPhoto GetBreweryPhoto(int brewID);
        int GetLastAddedBrewPhotoID(int brewID);



    }
}
