using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ImageSharingWithUpload.Controllers
{
    public class AccountController : Controller
    {
        protected void CheckAda()
        {
            var cookie = Request.Cookies["ADA"];
            if (cookie != null && "on".Equals(cookie)) // The value of checkbox is either "on" or NULL, it won't be true or false
            {
                ViewBag.isADA = true;
            }
            else
            {
                ViewBag.isADA = false;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            CheckAda();
            return View();
        }

        [HttpPost]
        public ActionResult Register(String Userid, String ADA)
        {
            CheckAda();
            var options = new CookieOptions() { IsEssential = true, Expires = DateTime.Now.AddMonths(3), Secure = true };

            // TODO add cookies for "Userid" and "ADA"

            Response.Cookies.Append("Userid", Userid, options);

            if (ADA != null ){
                Response.Cookies.Append("ADA", ADA, options);
            }
            else
            {
                Response.Cookies.Delete("ADA", options);
            }
            
            // End TODO

            ViewBag.Userid = Userid;
            return View("RegisterSuccess");
        }

    }
}