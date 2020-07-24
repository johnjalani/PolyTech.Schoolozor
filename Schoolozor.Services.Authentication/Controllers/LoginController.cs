using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel;
using Schoolozor.Services.Authentication.Services;
using Schoolozor.Shared.Controller;
using Schoolozor.Shared.Model;
using Schoolozor.Shared.Routes;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Schoolozor.Services.Authentication.Controller
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route(VersionOne.Base + "security/login")]
    [ApiController]
    public class LoginController : SchoolBaseController
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ResponseResult<LoginResult>> Login(Credentials user)
        {
            try
            {
                LoginResult result = await _loginService.LoginUserAsync(user);
                return ResponseResult<LoginResult>.SetSuccess(result);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                throw;
            }
        }
    }

    #region SERVICES
    public interface ILoginService
    {
        Task<LoginResult> LoginUserAsync(Credentials user);
    }
    public class LoginService : ILoginService
    {
        private readonly UserManager<SchoolUser> _user;
        private readonly SecurityServices _security;
        public LoginService(
            UserManager<SchoolUser> user,
            SecurityServices security)
        {
            _user = user;
            _security = security;
        }
        public async Task<LoginResult> LoginUserAsync(Credentials user)
        {
            var userAccount = await _user.FindByNameAsync(user.Username);
            if (userAccount != null)
            {
                var isValidUser = await _user.CheckPasswordAsync(userAccount, user.Password);
                if (isValidUser)
                {
                    return _security.GetLoginResult(userAccount);
                }
            }
            return new LoginResult();
        }
    } 
    #endregion
}
