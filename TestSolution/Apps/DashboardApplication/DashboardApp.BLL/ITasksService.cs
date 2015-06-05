using System;
using System.Collections.Generic;
using DashboardApp.Common.Models;

namespace DashboardApp.BLL
{
    public interface ITasksService
    {
        IEnumerable<Task> Tasks { get; }
        void Add(Task task);
        bool Exists(Guid id);
        void Delete(Guid id);
        void Modify(Task task);
    }
}