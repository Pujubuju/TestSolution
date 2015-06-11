using System;
using System.Collections.Generic;
using System.Linq;
using DashboardApp.Common.Models;

namespace DashboardApp.BLL.Services
{
    public class TasksService : ITasksService
    {

        #region Fields and Properties

        public IEnumerable<Task> Tasks
        {
            get { return _tasks; }
        }

        private readonly List<Task> _tasks = new List<Task>();

        #endregion Fields and Properties

        #region Constructor

        public TasksService()
        {
            _tasks = new List<Task>
            {
                new Task
                {
                    Id = Guid.NewGuid(),
                    Name = "ASP MVC",
                    Description = "MVC learning",
                    EstimatedTime = TimeSpan.FromHours(3)
                },
                new Task
                {
                    Id = Guid.NewGuid(),
                    Name = "NHibernate",
                    Description = "NHibernate learning",
                    EstimatedTime = TimeSpan.FromHours(8)
                },
                new Task
                {
                    Id = Guid.NewGuid(),
                    Name = "Supcom",
                    Description = "just fun :)",
                    EstimatedTime = TimeSpan.FromHours(2)
                },
            };
        }

        #endregion Constructor

        #region ITasksService

        public void Add(Task task)
        {
            task.Id = Guid.NewGuid();
            _tasks.Add(task);
        }

        public bool Exists(Guid id)
        {
            return _tasks.Exists(x => x.Id == id);
        }

        public void Delete(Guid id)
        {
            if (Exists(id))
            {
                Task task = _tasks.Single(x => x.Id == id);
                _tasks.Remove(task);
            }
        }

        public void Modify(Task task)
        {
            if (Exists(task.Id))
            {
                Task taskToEdit = _tasks.Single(x => x.Id == task.Id);
                taskToEdit.Name = task.Name;
                taskToEdit.Description = task.Description;
                taskToEdit.EstimatedTime = task.EstimatedTime;
            }
        }

        #endregion ITasksService

    }
}
