using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using Microsoft.AspNetCore.Hosting;

using ImageSharingWithUpload.Models;

namespace ImageSharingWithUpload.Controllers
{
    public class HomeController : Controller
    {
        protected void CheckAda()
        {
            var cookie = Request.Cookies["ADA"];
            if (cookie != null && "on".Equals(cookie))
            {
                ViewBag.isADA = true;
            }
            else
            {
                ViewBag.isADA = false;
            }
        }

        [HttpGet]
        public IActionResult Index(String id = "Stranger")
        {
            CheckAda();
            ViewBag.Title = "Welcome!";
            ViewBag.Id = id;
            return View();
        }

        //public IActionResult Index(String Id)
        //{
        //    CheckAda();
        //    var cookie = Request.Cookies["Userid"];
        //    ViewBag.Title = "Welcome!";
        //    ViewBag.Id = cookie;
        //    return View();
        //}vis
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
