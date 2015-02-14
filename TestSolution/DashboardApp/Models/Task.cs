using System;

namespace DashboardApp.Models
{
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedTime { get; set; }
    }
}