using System.Collections.Generic;
using DashboardApp.Common.Models;

namespace DashboardApp.BLL
{
    public interface ITasksService
    {
        IEnumerable<Task> Tasks { get; }
        void Add(Task task);
    }
}