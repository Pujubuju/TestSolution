using System;
using System.Collections.Generic;
using System.Linq;
using DashboardApp.Common.Models;

namespace DashboardApp.BLL.Services
{
    public class UsersService : IUsersService
    {
        #region Fields and Properties

        public IEnumerable<User> Users
        {
            get { return _users; }
        }

        private readonly List<User> _users = new List<User>();

        #endregion Fields and Properties

        #region Constructor

        public UsersService()
        {
            _users = new List<User>();
        }

        #endregion Constructor

        #region ITasksService

        public void Add(User task)
        {
            task.Id = Guid.NewGuid();
            _users.Add(task);
        }

        public bool Exists(Guid id)
        {
            return _users.Exists(x => x.Id == id);
        }

        public void Delete(Guid id)
        {
            if (Exists(id))
            {
                User task = _users.Single(x => x.Id == id);
                _users.Remove(task);
            }
        }

        public void Modify(User task)
        {
            if (Exists(task.Id))
            {
                User taskToEdit = _users.Single(x => x.Id == task.Id);
                taskToEdit.Email = task.Email;
                taskToEdit.Password = task.Password;
            }
        }

        #endregion ITasksService
    }
}
