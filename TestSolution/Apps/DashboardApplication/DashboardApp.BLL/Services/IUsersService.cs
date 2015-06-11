using System;
using System.Collections.Generic;
using DashboardApp.Common.Models;

namespace DashboardApp.BLL.Services
{
    public interface IUsersService
    {
        IEnumerable<User> Users { get; }
        void Add(User task);
        void Modify(User task);
        void Delete(Guid id);
    }
}