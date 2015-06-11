using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DashboardApp.BLL.Services;
using DashboardApp.Common.Models;

namespace DashboardApp.WebApi.Controllers
{
    public class RegistrationController : ApiController
    {
         #region Fields and Properties

        private readonly IUsersService _usersService;

        #endregion Fields and Properties

        #region Constructor

        public RegistrationController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _usersService.Users;
        }

        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            User user = _usersService.Users.FirstOrDefault((p) => p.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IHttpActionResult Post(User user)
        {
            if (user == null)
            {
                return NotFound();
            }
            _usersService.Add(user);
            return Ok(user);
        }

        [HttpPut]
        public IHttpActionResult Put(User user)
        {
            if (user == null)
            {
                return NotFound();
            }
            _usersService.Modify(user);
            return Ok(user);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            _usersService.Delete(id);
            return Ok(id);
        }

        #endregion Methods
    }
}
