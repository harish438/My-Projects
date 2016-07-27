using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.userAccount.ToList());
            }
        }
        // [Http:GET]  Will get only Registration Form 
        public ActionResult Register()      // This will respond if GET request
        {
            return View();
        }
        // [Http: POST]   Will send the details to Server
        [HttpPost]
        public ActionResult Register(UserAccount account)     // This will respond when POST request
        {
            if(ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                 }
                ModelState.Clear();
                ViewBag.Message = "Hello " + account.FirstName + " " + account.LastName + ", " + "Welcome to MVC World";
            }
            return View();
        }
        public ActionResult LogIn()     // Login Page .... GET Request
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(UserAccount user)      // we will give Login details to server....POST Request
        {
            using (OurDbContext db = new OurDbContext())
            {
                // we can use this statement also.
                //x => (string.Compare(x.UserName, user.UserName, false) == 0) && (string.Compare(x.Password, user.Password, false) == 0)

                var usr = db.userAccount.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserId"] = user.Userid.ToString();
                    Session["UserName"] = user.UserName.ToString();
                    FormsAuthentication.SetAuthCookie(user.UserName,true);
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Please Enter valid Username & Password");
                }
            }
            return View();
        }
        //********************************************
        //[HttpPost]
        //public ActionResult Login(Models.UserAccount user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (user.IsValid(user.UserName, user.Password))
        //        {
        //            FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
        //            return RedirectToAction("LoggedIn", "Account");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Login data is incorrect!");
        //        }
        //    }
        //    return View(user);
        //}
        // ********************************************************************
        public ActionResult LoggedIn()
        {
            if(Session["UserId"]!=null)
            {
                return View();
             }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
       // clear authentication cookie
       //     HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
       //     cookie1.Expires = DateTime.Now.AddYears(-1);
       //     Response.Cookies.Add(cookie1);
       //// clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
       //     HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
       //     cookie2.Expires = DateTime.Now.AddYears(-1);
       //     Response.Cookies.Add(cookie2);
       //// Invalidate the Cache on the Client Side
       //     Response.Cache.SetCacheability(HttpCacheability.NoCache);
       //     Response.Cache.SetNoStore();

       // Redirect to the Home Page (that should be intercepted and redirected to the Login Page first)
            return RedirectToAction("LogIn","Account");
        }
    }
}