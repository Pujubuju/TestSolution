using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DashboardApp.Models;

namespace DashboardApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home
        public ActionResult Dupa()
        {
            return View();
        }

        // GET: Home
        public ActionResult List()
        {
            var taskCollection = new TaskCollection
            {
                Tasks = new List<Task>
                {
                    new Task
                    {
                        Name = "ASP MVC", 
                        Description = "MVC learning",
                        EstimatedTime = TimeSpan.FromHours(3)
                    },
                    new Task
                    {
                        Name = "NHibernate",
                        Description = "NHibernate learning",
                        EstimatedTime = TimeSpan.FromHours(8)
                    },
                    new Task
                    {
                        Name = "Supcom", 
                        Description = "just fun :)", 
                        EstimatedTime = TimeSpan.FromHours(2)
                    },
                }
            };
            return View(taskCollection);
        }
    }
}