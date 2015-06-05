using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DashboardApp.BLL;
using DashboardApp.Common.Models;

namespace DashboardApp.WebApi.Controllers
{
    public class TasksController : ApiController
    {

        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _tasksService.Tasks;
        }

        public IHttpActionResult GetTask(Guid id)
        {
            Task task = _tasksService.Tasks.FirstOrDefault((p) => p.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

    }
}
