using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC5WebApplication.Models;

namespace MyFirstMVC5WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var person = new Person("John Rambo", 23);


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}