using MojaAplikacja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;



namespace MojaAplikacja.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User u)
        {
            if(ModelState.IsValid)
            {
                using (MyDatabaseEntities3 dc = new MyDatabaseEntities3())
                {
                    var v = dc.Users.Where(a => a.UserName == u.UserName).SingleOrDefault();
                    if(v == null)
                    {
                        dc.Users.Add(u);
                        dc.SaveChanges();
                        return View("AfterRegistration");
                    }
                    else
                    {
                        ModelState.AddModelError("","User Already Exists");
                    }
                    
                }
            }
            
            return View(u);
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(UserLogin u)
        {
            string message = "";
            if(ModelState.IsValid)
            {
                using (MyDatabaseEntities3 dc = new MyDatabaseEntities3())
                {
                    var v = dc.Users.SingleOrDefault(a => a.UserName == u.UserName);

                    if(v != null)
                    {

                        if (string.Compare(u.Password,v.Password)==0)
                        {
                            FormsAuthentication.SetAuthCookie(u.UserName, u.RememberMe);
                            Session["UserName"] = u.UserName;
                            if (u.RememberMe)
                            {
                                HttpCookie cookie = new HttpCookie("UserLogin");
                                cookie.Values.Add("UserName", u.UserName);
                                cookie.Values.Add("Password", u.Password);
                                cookie.Expires = DateTime.Now.AddDays(15);
                                Response.Cookies.Add(cookie);
                            }
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "WRONG");
                        }
                    }
                    else
                    {
                        message = "wuuut";
                    }
                }
            }
            return View(u);
        }
        public ActionResult AfterRegistration()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ShowProfile(string id)
        {
            using (MyDatabaseEntities3 dc = new MyDatabaseEntities3())
            {
                var username = Session["UserName"].ToString();
                var v = dc.Users.Where(a => a.UserName == username).SingleOrDefault();
                if (v != null)
                {
                    return View(v);
                }


            }
                return View();
        }
        

    }
}