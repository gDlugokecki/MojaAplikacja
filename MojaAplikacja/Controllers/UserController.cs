using MojaAplikacja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using System.Data.Entity;


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
                using (MyDatabaseEntities5 dc = new MyDatabaseEntities5())
                {
                    var v = dc.User.Where(a => a.UserName == u.UserName).SingleOrDefault();
                    if(v == null)
                    {
                        dc.User.Add(u);
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
                using (MyDatabaseEntities5 dc = new MyDatabaseEntities5())
                {
                    var v = dc.User.SingleOrDefault(a => a.UserName == u.UserName);

                    if(v != null)
                    {

                        if (string.Compare(u.Password,v.Password)==0)
                        {
                            FormsAuthentication.SetAuthCookie(u.UserName, u.RememberMe);
                            Session["UserName"] = v.UserName;
                            Session["UserID"] = v.UserID;
                            if (u.RememberMe)
                            {
                                HttpCookie cookie = new HttpCookie("UserLogin");
                                cookie.Values.Add("UserName",v.UserName);
                                cookie.Values.Add("UserID", v.UserID.ToString());
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
            using (MyDatabaseEntities5 dc = new MyDatabaseEntities5())
            {

                var username = "";
                if (Session["Username"] == null)
                {
                    username = Request.Cookies["UserLogin"].Value.ToString();
                    username = username.Substring(username.IndexOf("=")+1);
                    username = username.Remove(username.IndexOf("&"));
                }
                else
                {
                     username = Session["UserName"].ToString();
                }

                var v = dc.User.Where(a => a.UserName == username).SingleOrDefault();
                if (v != null)
                {
                    return View(v);
                }


            }
            return View();
        }
        public ActionResult AddPhoto(HttpPostedFileBase file)
        {
            using (MyDatabaseEntities5 dc = new MyDatabaseEntities5())
            {
                if (file != null)
                {
                    var Id = "";
                    if (Session["UserID"] == null)
                    {
                        Id = Request.Cookies["UserLogin"].Value.ToString();
                        Id = Id.Substring(Id.IndexOf("&") + 1);
                        Id = Id.Substring(Id.IndexOf("=") + 1);
                    }
                    else
                    {
                        Id = Session["UserID"].ToString();
                    }
                    var v = dc.User.Where(a => a.UserID.ToString() == Id).SingleOrDefault();
                    if (v != null)
                    {

                        string ImageName = System.IO.Path.GetFileName(file.FileName);
                        string physicalPath = System.IO.Path.Combine(Server.MapPath("~/Images/"), ImageName);
                        string filename = file.FileName;
                        string relativeFileName = "~/Images/" + ImageName;
                        file.SaveAs(physicalPath);
                        v.PhotoPath = relativeFileName;
                        dc.User.Attach(v);
                        dc.Entry(v).State = EntityState.Modified;
                        dc.SaveChanges();

                    }
                    
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult ChangePassword(string Password)
        {
            using (MyDatabaseEntities5 dc = new MyDatabaseEntities5())
            {
               
                    var Id = "";
                    if (Session["UserID"] == null)
                    {
                        Id = Request.Cookies["UserLogin"].Value.ToString();
                        Id = Id.Substring(Id.IndexOf("&") + 1);
                        Id = Id.Substring(Id.IndexOf("=") + 1);
                    }
                    else
                    {
                        Id = Session["UserID"].ToString();
                    }
                    var v = dc.User.Where(a => a.UserID.ToString() == Id).SingleOrDefault();
                    if (v != null)
                    {
                        v.Password = Password;
                        dc.User.Attach(v);
                        dc.Entry(v).State = EntityState.Modified;
                        dc.SaveChanges();
                    }
            }
                        return PartialView();
        }



    }
}