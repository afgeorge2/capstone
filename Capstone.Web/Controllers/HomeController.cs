using Capstone.Web.DAL;
using Capstone.Web.Models;
using Capstone.Web.Models.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult UpdateBreweryInfo(string history, string address, string cname, string email, string phone)
        {
            Brewery b = new Brewery
            {
                History = history,
                Address = address,
                ContactName = cname,
                ContactEmail = email,
                ContactPhone = phone
            };
            b.BreweryID = 1;
            _brew.UpdateBreweryInfo(b);

            return View();
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
        public ActionResult AddBeer(Beer b)
        {
            
            _brew.AddNewBeer(b);

            return Redirect("BreweryInformation");
        }

        #endregion

        public ActionResult UserRegistration()
        {
            return View("UserRegistration");
        }

        [HttpPost]
        public ActionResult UserRegistration(string username, string password, string email)
        {
            User newUser = new User();
            newUser.UserName = username;
            newUser.Password = password;
            newUser.EmailAddress = email;
            _brew.UserRegistration(newUser);
            SessionKey.Email = newUser.EmailAddress;
            return View("Index", newUser);
        }




    }
}