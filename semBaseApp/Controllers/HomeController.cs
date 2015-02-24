using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace vls.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return User.Identity.GetUserId() != null ? View("Index") : View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [Authorize(Roles = "Agent")]
        public ActionResult Send()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [Authorize(Roles = "Agent")]
        public ActionResult Recieve()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [Authorize(Roles = "Agent")]
        public ActionResult Cancel()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Reverse()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}
