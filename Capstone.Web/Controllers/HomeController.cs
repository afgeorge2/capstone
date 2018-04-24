using Capstone.Web.DAL;
using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IBreweryDBS _brew;

        public HomeController(IBreweryDBS brew)
        {
            _brew = brew;
        }



        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult UserLogin(string userName, string password)
        {
            User thisGuy = new User();

            return View("Index", thisGuy); 
        }



        public ActionResult AddBrewery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBreweryNewUser(string breweryName, string username, string Email, string Password)
        {
            int breweryID = _brew.AddNewBrewery(breweryName);

            _brew.AddNewBrewer(username, Password, true, breweryID, Email);

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult AddUserExistingBrewery(string username, int brewID, string Email, string Password)
        {
            _brew.AddNewBrewer(username, Password, true, brewID, Email);

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult GetAllBreweries()
        {
            var breweries = _brew.GetAllBrewerys();

            return Json(breweries, JsonRequestBehavior.AllowGet);
        }

        

    }
}