using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class UserDAL : Controller
    {
        private readonly string connectionString;

        public UserDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // GET: User
        public ActionResult Index()
        {
            if (Session[SessionKey.Email] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        // GET: User/Login
        public ActionResult Login()
        {
            return View("UserLogin");
        }

        [HttpPost]
        public ActionResult UserLogin(string email)
        {
            Web.UserDAL dal = new Web.UserDAL();

            User thisGuy = dal.GetUser(email);

            return View("Index", thisGuy);
        }
    }
}