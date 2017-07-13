using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojaAplikacja.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return RedirectToAction("Registration", "User");
        }
        public ActionResult LogIn()
        {
            return RedirectToAction("LogIn", "User");
        }
    }
}