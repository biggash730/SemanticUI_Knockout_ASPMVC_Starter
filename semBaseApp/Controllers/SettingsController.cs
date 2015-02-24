using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace vls.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}