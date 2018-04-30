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
            return View();
        }

        public ActionResult BreweryDetail(int brewID)
        {
            Brewery brewDetail = _brew.GetBreweryByID(brewID);
            return View("BreweryDetail", brewDetail);
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
            ,string open0, string close0, string open1, string close1, string open2, string close2, string open3, string close3, string open4, string close4, string open5, string close5, string open6, string close6
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
                    Brewery brew = _brew.GetBreweryByID(picbrewID);

                    var filename = $"{brew.BreweryName}.jpg";

                    var path = Path.Combine(Server.MapPath("~/Photos"), filename);

                    _brew.UploadBreweryPhoto(filename, brew.BreweryID,true);

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

        public ActionResult BeerDetail(Beer model)
        {
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
            return View();
        }

        //add beer post
        [HttpPost]
        public ActionResult AddBeer(AddBeerModel b, int brewId)
        {
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
            int brewId = (int)Session["BreweryId"];
            DeleteBeer b = new DeleteBeer();
            b.DropDownBeers = _brew.GetAllBeersFromBrewery(brewId);

            return View("DeleteBeer", b);
        }
        //delete beer post
        [HttpPost]
        public ActionResult DeleteBeer(DeleteBeer b, int brewId)
        {
            b.BreweryId = brewId;
            _brew.DeleteBeer(b);
            return Redirect("AddBeer");
        }

        //update beer availability (show/hide)

        public ActionResult ShowHideBeer()
        {
            //List<Beer> beerlist = _brew.GetAllBeersFromBrewery((int)Session["breweryId"]);
            List<Beer> beerlist = _brew.GetAllBeersFromBrewery(1);

            return View(beerlist);
        }

        [HttpPost]
        public ActionResult ShowHideBeer(int brewId)
        {
            //_brew.UpdateShowHide(List<Beer> beers);

            return View();
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
            catch(Exception ex)
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