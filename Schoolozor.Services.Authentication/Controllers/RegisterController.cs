using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel;
using Schoolozor.Services.Authentication.Controller;
using Schoolozor.Services.Authentication.Services;
using Schoolozor.Shared.Controller;
using Schoolozor.Shared.Model;
using Schoolozor.Shared.Routes;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Schoolozor.Services.Authentication.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route(VersionOne.Base + "security/register")]
    [ApiController]
    public class RegisterController : SchoolBaseController
    {
        private readonly IRegisterService _register;
        public RegisterController(IRegisterService register)
        {
            _register = register;
        }
        [Route("")]
        [AllowAnonymous]
        public async Task<ResponseResult<LoginResult>> Register(UserRegistration user)
        {
            LoginResult result = await _register.RegisterUserAsync(user);
            return ResponseResult<LoginResult>.SetSuccess(result);
        }
        [Route("test")]
        [AllowAnonymous]
        public UserRegistration Test()
        {
            return new UserRegistration();
        }
    }

    #region SERVICES
    public interface IRegisterService {
        Task<LoginResult> RegisterUserAsync(UserRegistration user);

    }
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<SchoolUser> _user;
        private readonly SecurityServices _security;
        public RegisterService(
            UserManager<SchoolUser> user,
            SecurityServices security)
        {
            _user = user;
            _security = security;
        }
        public async Task<LoginResult> RegisterUserAsync(UserRegistration user)
        {
            try
            {

                var userAccount = new SchoolUser()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName
                };
                var result = await _user.CreateAsync(userAccount, user.Password);

                if (result.Succeeded)
                {
                    return _security.GetLoginResult(userAccount);
                }

                return new LoginResult();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }
    }
    #endregion
}
