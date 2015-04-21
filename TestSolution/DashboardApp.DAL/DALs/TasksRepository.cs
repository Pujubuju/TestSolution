using System.Data.Entity;
using DashboardApp.Common.Models;

namespace DashboardApp.DAL.DALs
{
    public class TasksRepository : DbContext
    {

        public DbSet<Task> Tasks { get; set; }

        public TasksRepository():base()
        {
            
        }



    }
}
