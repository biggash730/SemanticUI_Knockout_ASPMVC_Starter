﻿using System.Web.Mvc;

namespace BackEnd.Controllers
{
    public class UsersController : Controller
    {
        /*// GET: VendorNetworks
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Home");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }*/
    }
}