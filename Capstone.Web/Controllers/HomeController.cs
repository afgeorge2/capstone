using Capstone.Web.DAL;
using Capstone.Web.DAL.Interfaces;
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
        private IBreweryDBS _brew;

        public HomeController(IBreweryDBS brew)
        {
            _brew = brew;
        }

        public HomeController()
        {
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
            //BrewerBrewery model = new BrewerBrewery();
            return View("AddBrewery");
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


        [HttpGet]
        public ActionResult GetAllBreweries()
        {
            var breweries = _brew.GetAllBrewerys();

            return Json(breweries, JsonRequestBehavior.AllowGet);
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


        

    }
}