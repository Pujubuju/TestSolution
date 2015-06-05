using DashboardApp.Common.Models;

namespace DashboardApp.BLL
{
    public interface ITasksService
    {
        TaskCollection Tasks { get; }
    }
}