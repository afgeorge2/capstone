using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDAL _userDAL;

        public UserController(IUserDAL userDAL)
        {
            _userDAL = userDAL;
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
        public ActionResult UserLogin(LoginViewModel model)
        {
            Web.UserDAL dal = new Web.UserDAL();

            User thisGuy = dal.GetUser(email);
            Session["BreweryId"] = thisGuy.BreweryId;
            User thisGuy = dal.GetUser(model.EmailAddress);

            if(model.Password == thisGuy.Password)
            {
                FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
                Session[SessionKey.Email] = thisGuy.EmailAddress;
                Session[SessionKey.UserID] = thisGuy.UserName;
                return View("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
    }
}