using System;
using System.Linq;
using System.Web.Mvc;
using DashboardApp.BLL;

namespace DashboardApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly ITasksService _tasksService;

        public HomeController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

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
            return View(_tasksService.Tasks);
        }

        public ActionResult Details(Guid taskId)
        {
            return View(_tasksService.Tasks.Tasks.Single(x => x.Id == taskId));
        }


    }
}