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
        public ActionResult LogOut()
        {
            return RedirectToAction("LogOut", "User");
        }
        public ActionResult ShowProfile()
        {
            return RedirectToAction("ShowProfile", "User");
        }
        public ActionResult Forum()
        {
            return RedirectToAction("Forum", "User");
        }
    }
}