using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_Tutorial.Controllers
{
    public class LogOutController : Controller
    {
        // GET: LogOut
        public ActionResult Index()
        {
            Session.Clear();
            Session.Abandon();

            FormsAuthentication.SignOut();
            // clear authentication cookie
            /* HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
             cookie1.Expires = DateTime.Now.AddYears(-1);
             Response.Cookies.Add(cookie1);*/
            // Response.Cookies.Remove("ASP.NET_SessionId");

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            /*  HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
              cookie2.Expires = DateTime.Now.AddYears(-1);
              Response.Cookies.Add(cookie2);*/
            return RedirectToAction("LoggedOut");
        }

        public ActionResult LoggedOut()
        {
            return View();
        }
    }
}