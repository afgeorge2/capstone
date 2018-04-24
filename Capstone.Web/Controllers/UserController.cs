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
        //private readonly IUserDAL _userDAL;

        //public UserController(IUserDAL userDAL)
        //{
        //    _userDAL = userDAL;
        //}

       
        //public ActionResult Index()
        //{
        //    if (Session[SessionKey.Email] == null)
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
        

        //public ActionResult Login()
        //{
        //    return View("Login");
        //}

        //[HttpPost]
        //public ActionResult Login(LoginViewModel model)
        //{

        //    User thisGuy = _userDAL.GetUser(model.EmailAddress);
        //    Session["BreweryId"] = thisGuy.BreweryId;

        //    if(model.Password == thisGuy.Password)
        //    {
        //        FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
        //        Session[SessionKey.Email] = thisGuy.EmailAddress;
        //        Session[SessionKey.UserID] = thisGuy.UserName;
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "User");
        //    }
            
        //}
    }
}