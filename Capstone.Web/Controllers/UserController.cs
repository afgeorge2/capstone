using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserDAL dal;


        public object SessionKeys { get; private set; }

        // GET: User
        public ActionResult Index()
        {
            if (Session[SessionKeys.] == null)
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
        public ActionResult UserLogin(string userName, string password)
        {
            //UserDAL dal = new UserDAL();

            User thisGuy = dal.GetUser(userName, password);

            return View("Index", thisGuy);
        }
        [HttpPost]
        public ActionResult AddUser(string username, string password, string email)
        {
            //UserDAL dal = new UserDAL();
            User user = new User();
            user.EmailAddress = email;
            user.UserName = username;
            user.Password = password;
            dal.UserRegistration(user);
            return View("Index");

            
        }
    }
}