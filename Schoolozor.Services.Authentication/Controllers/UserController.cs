using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model;
using Schoolozor.Services.Authentication.Services;
using Schoolozor.Shared.Controller;
using Schoolozor.Shared.Model;
using Schoolozor.Shared.Routes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Services.Authentication.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route(VersionOne.Base + "security/users")]
    [ApiController]
    public class UserController : SchoolBaseController
    {
        private readonly IUserService _user;
        public UserController(IUserService user)
        {
            _user = user;
        }

        [Route("")]
        public ResponseResult<IEnumerable<SchoolUser>> Users()
        {
            return ResponseResult<IEnumerable<SchoolUser>>.SetSuccess(_user.GetUsers());
        }
        [Route("{Id}")]
        public ResponseResult<SchoolUser> Users(string Id)
        {
            return ResponseResult<SchoolUser>.SetSuccess(_user.GetUser(Id));
        }
    }

    #region SERVICES
    public interface IUserService {
        SchoolUser GetUser(string Id);
        IEnumerable<SchoolUser> GetUsers();
    }
    public class UserService : IUserService
    {
        private readonly SecurityServices _security;
        public UserService(SecurityServices security)
        {
            _security = security;
        }
        public SchoolUser GetUser(string Id)
        {
            return _security.GetUser(Id);
        }

        public IEnumerable<SchoolUser> GetUsers()
        {
            return _security.GetUsers();
        }
    }
    #endregion
}
