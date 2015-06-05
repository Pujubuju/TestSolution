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

        [HttpGet]
        public IEnumerable<Task> GetAllTasks()
        {
            return _tasksService.Tasks;
        }

        [HttpGet]
        public IHttpActionResult GetTask(Guid id)
        {
            Task task = _tasksService.Tasks.FirstOrDefault((p) => p.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public IHttpActionResult PostTask(Task task)
        {           
            if (task == null)
            {
                return NotFound();
            }
            _tasksService.Add(task);
            return Ok(task);
        }

        [HttpPut]
        public IHttpActionResult PutTask(Task task)
        {
            if (task == null)
            {
                return NotFound();
            }
            _tasksService.Modify(task);
            return Ok(task);
        }

        [HttpDelete]
        public IHttpActionResult DeleteTask(Guid id)
        {
            _tasksService.Delete(id);
            return Ok(id);
        }
    }
}
