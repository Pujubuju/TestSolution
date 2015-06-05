using System;

namespace DashboardApp.Common.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedTime { get; set; }
    }
}