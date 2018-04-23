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
        public ActionResult AddBrewery(string breweryName)
        {
            _brew.AddNewBrewery(breweryName);

            return View();
        }


        [HttpPost]
        public ActionResult LinkUserBrewery(int userID, int brewID)
        {
            _brew.LinkBrewerToBrewery(userID, brewID);

            return View();
        }


    }
}