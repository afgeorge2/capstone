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

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult UserLogin(string userName, string password)
        {

            UserDAL DAL = new UserDAL();
            User thisGuy = DAL.GetUser(userName, password);

            return View("Index", thisGuy);
        }
    }
}