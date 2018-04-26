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
        }

        public HomeController()
        {

        }

        #endregion

        #region --- Home Page (INDEX) ---


        public ActionResult Index()
        {
            return View();
        }


        #endregion


        #region --- Brewery Actions ---


        public ActionResult AddBrewery()
        {
            return View("AddBrewery");
        }

        [HttpGet]
        public ActionResult GetAllBreweries()
        {
            var breweries = _brew.GetAllBrewerys();

            return Json(breweries, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddBreweryNewUser(BrewerBrewery m, int? brewID)
        {
            if (m.BreweryName!=null)
            {
                m.BreweryID = _brew.AddNewBrewery(m.BreweryName);
            }
            if (!ModelState.IsValid)
            {
                return View("AddBrewery", m);
            }

            _brew.AddNewBrewer(m.UserName, m.Password, true, m.BreweryID, m.EmailAddress);

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult AddUserExistingBrewery(BrewerBrewery m)
        {

            _brew.AddNewBrewer(m.UserName, m.Password, true, m.BreweryID, m.EmailAddress);

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult UpdateBreweryInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateBreweryInfo(string history, string address, string cname, string email, string phone, int brewID, HoursViewModel m)
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
            _brew.UpdateBreweryHours(m);




            return View();
        }

        #endregion



        #region --- UplaodFile ---


        public ActionResult FileUpload()
        {
            return View();
        }




        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("image/png") || contentType.Equals("image/gif") ||
                contentType.Equals("image/jpg") || contentType.Equals("image/jpeg");
        }


        [HttpPost]
        public ActionResult Process(HttpPostedFileBase photo)
        {
            if (!isValidContentType(photo.ContentType))
            {
                ViewBag.Error = "wrong format";
                return View("FileUpload");
            }
            else
            {
                var filename = Path.GetFileName(photo.FileName);
                //var filename = "pic1";
                var path = Path.Combine(Server.MapPath("~/Photos"), filename);
                photo.SaveAs(path);
                return View("FileUpload");
            }
        }


        #endregion





        #region --- Add Beer Actions ---

        //add beer view
        public ActionResult AddBeer()
        {
            return View();
        }

        //add beer post
        [HttpPost]
        public ActionResult AddBeer(AddBeerModel b, int brewId)
        {
            b.BreweryId = brewId;
            _brew.AddNewBeer(b);

            return Redirect("Index");
        }

        //get all beer
        [HttpGet]
        public ActionResult GetAllBeers()
        {
            var beers = _brew.GetAllBeer();

            return View("Index", beers);
        }
        #endregion


        #region --- User Login/Register ---





        public ActionResult UserRegistration()
        {
            return View("UserRegistration");
        }

        [HttpPost]
        public ActionResult UserRegistration(User user)
        {
            if (user.UserName == null || user.Password == null || user.EmailAddress == null)
            {
                return View("UserRegistration");
            }
            _brew.UserRegistration(user);
            SessionKey.Email = user.EmailAddress;

            return RedirectToAction("Index");
           
        }

        //The following ActionResults are for checking if a user is in session, and then enabling them to 
        //log in, then return to the index view
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
        public ActionResult Login(LoginViewModel model)
        {

            string emailAddress = model.EmailAddress;
            User thisGuy = _brew.GetUser(emailAddress);

            if (model.Password == thisGuy.Password)
            {
                FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
                Session[SessionKey.Email] = thisGuy.EmailAddress;
                Session[SessionKey.UserID] = thisGuy.UserName;
                if (thisGuy.IsBrewer==true)
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
                return View("Login");//***For future, have js let user know this is incorrect 
            }

        }

        #endregion


    }
}