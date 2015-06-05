using System;
using System.Collections.Generic;
using DashboardApp.Common.Models;
using Task = DashboardApp.Common.Models.Task;

namespace DashboardApp.BLL
{
    public class TasksService : ITasksService
    {
        public TaskCollection Tasks { get; private set; }

        public TasksService()
        {
            Tasks = new TaskCollection
            {
                Tasks = new List<Task>
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
                }
            };
        }
    }
}
