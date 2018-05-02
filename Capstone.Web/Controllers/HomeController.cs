using Capstone.Web.DAL;
using Capstone.Web.Models;
using Capstone.Web.Models.Viewmodel;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        #region --- Contructors ---

        private IBreweryServiceDAL _brew;

        public HomeController(IBreweryServiceDAL brew)
        {
            _brew = brew;
            _brew.GetAllBeers();
        }

        public HomeController()
        {

        }

        #endregion

        #region --- Home Page (INDEX) ---

       

        public ActionResult Index()
        {
            List<Brewery> allBreweries = _brew.GetAllBrewerys();
            return View("Index",allBreweries);
        }




        public ActionResult Index1()
        {
            Brewery brew = _brew.GetBreweryByID(1);



            return View(brew);
        }


        #endregion


        #region --- Brewery Actions ---


        public ActionResult AddBrewery()
        {
            if (Session["Admin"] == null)
            {
                RedirectToAction("Index");
            }

            return View("AddBrewery");
        }

        public ActionResult NewBreweryNewUser(string bname1, string uname1, string e_mail1, string p_word1)
        {
            int breweryID = _brew.AddNewBrewery(bname1);

            _brew.AddNewBrewer(uname1, p_word1, true, breweryID, e_mail1);

            return RedirectToAction("Index");
        }


        public ActionResult NewUserCurrentBrewery(int brewID1, string uname2, string e_mail2, string p_word2)
        {

            _brew.AddNewBrewer(uname2, p_word2, true, brewID1, e_mail2);

            return RedirectToAction("Index");
        }


        public ActionResult CurrentUserNewBrewery(string bname2, string email3)
        {
            int breweryID = _brew.AddNewBrewery(bname2);

            _brew.UpdateUserBrewer(breweryID, email3);

            return Json(new { data = true });
        }


        public ActionResult CurrentUserCurrentBrewery(int brewID2, string email4)
        {
            _brew.UpdateUserBrewer(brewID2, email4);

            return Json(new { data = true });
        }

        [HttpGet]
        public ActionResult GetAllBreweries()
        {
            var breweries = _brew.GetAllBrewerys();

            return Json(breweries, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SearchUsers(string email)
        {
           User user = _brew.SearchUserToAddBrewery(email);

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCurrentBrewry(int brewID)
        {
            var breweries = _brew.GetBreweryByID(brewID);

            return Json(breweries, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Gethours(int brewID)
        {
            var hours = _brew.GetHoursForBrewery(brewID);

            return Json(hours, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateBreweryInfo()
        {
            if (Session["BreweryId"] == null)
            {
                RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult BreweryDetail(int brewID)
        {
            Brewery brewDetail = _brew.GetBreweryByID(brewID);
            return View("BreweryDetail", brewDetail);
        }

        public ActionResult AllBreweries()
        {
            List<Brewery> brews = _brew.GetAllBrewerys();
            return View(brews);
        }

        #endregion


        #region --- UplaodFile ---

        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("image/png") || contentType.Equals("image/gif") ||
                contentType.Equals("image/jpg") || contentType.Equals("image/jpeg");
        }

        [HttpPost]
        public ActionResult Process(HttpPostedFileBase photo, int picbrewID, string history, string address, string cname, string email, string phone, int brewID, HoursViewModel m
            ,string open0, string close0, string open1, string close1, string open2, string close2, string open3, string close3, string open4, string close4, string open5, string close5, string open6, string close6, bool profPIC = false
            )
        {

           

            Brewery b = new Brewery
            {
                History = history,
                Address = address,
                ContactName = cname,
                ContactEmail = email,
                ContactPhone = phone,
                BreweryID = brewID
            };

            _brew.UpdateBreweryInfo(b);
            m.BrewID = brewID;
            _brew.UpdateBreweryHours(brewID, open0, close0, open1, close1, open2, close2, open3, close3, open4, close4, open5, close5, open6, close6);


            if (photo!=null)
            {
                if (!isValidContentType(photo.ContentType))
                {
                    ViewBag.Error = "wrong format";
                    return View("FileUpload");
                }
                else
                {
                    int idTag = _brew.GetLastAddedBrewPhotoID(brewID);

                    idTag += 1;

                    Brewery brew = _brew.GetBreweryByID(picbrewID);

                    var filename = $"{brew.BreweryName}{idTag}.jpg";

                    var path = Path.Combine(Server.MapPath("~/Photos"), filename);

                    string checkfile = Path.GetFileName(photo.FileName);

                    _brew.UploadBreweryPhoto(filename, brew.BreweryID, profPIC);



                    //var fInfo = new FileInfo(path);

                    //if (fInfo.Exists)
                    //{
                    //    fInfo.Delete();
                    //}


                    photo.SaveAs(path);

                    return Redirect("Index");
                }
            }
            return RedirectToAction("Index");

        }



        public ActionResult TestPhoto()
        {

            Brewery brew = _brew.GetBreweryByID(1);


            return View(brew);
        }




        #endregion





        #region --- Beer Actions ---
        //Working on

        public ActionResult ManageBeers()
        {
            return View();
        }

        public ActionResult GetBeersForManaging(int brewID)
        {
            List<Beer> beers = _brew.GetAllBeersFromBrewery(brewID);
            
            return Json(beers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowHideBeer(int beerID, int showHide)
        {
            _brew.UpdateShowHide(beerID, showHide);

            return View();
        }


        public ActionResult BeerDetail(int beerID)
        {
            Beer model = _brew.GetBeersById(beerID);
            return View("BeerDetail", model);
        }

        [HttpGet]

        public ActionResult GetAllBeers()
        {
            List<Beer> beers = _brew.GetAllBeers();
            return View("GetAllBeers", beers);
  
        }


        //add beer view
        public ActionResult AddBeer()
        {
            Session["BreweryId"] = 1;

            return View();
        }

        //add beer post
        [HttpPost]
        public ActionResult AddBeer(AddBeerModel b, int brewId, string beertypes)
        {
            if (beertypes!="null")
            {
                b.BeerType = beertypes;
            }
            //This first check redirects the user to the home page if they are not a brewer
            if (Session["BreweryId"] == null)
            {
                RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View("AddBeer", b);
            }

            b.BreweryId = brewId;
            _brew.AddNewBeer(b);
            return Redirect("Index");
        }

        //---------DELETE BEERS------------
        public ActionResult DeleteBeer()
        {
            if (Session["BreweryId"] == null)
            {
                RedirectToAction("Index");
            }

            int brewId = (int)Session["BreweryId"];
            DeleteBeer b = new DeleteBeer();
            b.DropDownBeers = _brew.GetAllBeersFromBrewery(brewId);
            return View("DeleteBeer", b);
        }

        public ActionResult GetBeersToDelete()
        {
            int brewId = (int)Session["BreweryId"];
            DeleteBeer b = new DeleteBeer();
            b.DropDownBeers = _brew.GetAllBeersFromBrewery(brewId);

            return Json(b, JsonRequestBehavior.AllowGet);
        }
        //delete beer post
        [HttpPost]
        public ActionResult DeleteBeer(DeleteBeer b, int brewId, string beername)
        {
            b.BreweryId = brewId;
            b.Name = beername;
            _brew.DeleteBeer(b);
            return Redirect("DeleteBeer");
        }


        //This action directs to a view that lists all of the beers made by a specific brewery
        public ActionResult BreweryBeerDetail(int brewID)
        {
            // (note from mataan) I think this View should have a single bber passed in to it if im thinking about this correctly? 

            List<Beer> beerlist = _brew.GetAllBeersFromBrewery(brewID);
            return View("BreweryBeers", beerlist);
        }




        public ActionResult BreweryBeers()
        {
            int brewID = 1;

            List<Beer> beerlist = _brew.GetAllBeersFromBrewery(brewID);

            return View(beerlist);
        }

        //Review a beer

        [HttpPost]
        public ActionResult ReviewBeer(int rating, string review, int userId, int beerId)
        {
            ReviewModel m = new ReviewModel();
            m.Rating = rating;
            m.ReviewPost = review;
            m.UserId = userId;
            m.BeerId = beerId;
            _brew.AddBeerReview(m);

            return Redirect("BeerDetail");
        }


        #endregion


        #region --- User Login/Register ---





        public ActionResult UserRegistration()
        {
            return View("UserRegistration");
        }

        [HttpPost]
        public ActionResult UserRegistration(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("UserRegistration", model);
                }


                User userExists = _brew.GetUser(model.EmailAddress); 

                if (userExists.EmailAddress == model.EmailAddress)
                {
                    ModelState.AddModelError("username-exists", "That email address is not available");
                    return View("UserRegistration");
                }
                else
                {
                    User users = new User()
                    {
                        EmailAddress = model.EmailAddress,
                        UserName = model.UserName,
                        Password = model.Password
                    };

                    FormsAuthentication.SetAuthCookie(users.EmailAddress, true);
                    _brew.UserRegistration(users);
                    SessionKey.Email = users.EmailAddress;
                    return RedirectToAction("Index");
                }

            }
            catch(Exception)
            {
                ModelState.AddModelError("username-exists", "An error occurred");
                return View("UserRegistration");
            }
        }
  
        public ActionResult Login()
        {
            if (Session[SessionKey.Email] == null)
            {
                return View("Login");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(SessionKey.Email);
            Session.Remove(SessionKey.UserID);
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            if (model.EmailAddress == "" || model.EmailAddress == null )
            {
                return View("Login", model);
            }

            string emailAddress = model.EmailAddress;

            User thisGuy = _brew.GetUser(emailAddress);
            if (thisGuy == null || thisGuy.Password != model.Password)
            {
                ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                return View("Login", model);
            }

            if (model.Password == thisGuy.Password && model != null)
            {
                FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
                Session[SessionKey.Email] = thisGuy.EmailAddress;
                Session[SessionKey.UserID] = thisGuy.UserName;
                if (thisGuy.IsBrewer == true)
                {
                    Session["BreweryId"] = thisGuy.BreweryId;
                }
                if (thisGuy.IsAdmin)
                {
                    Session["Admin"] = true;
                }
                else
                {
                    Session["Admin"] = null;
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login", model);
            }

        }

        #endregion




        #region --- New ADD Brewery Calls ---





        #endregion












    }

}