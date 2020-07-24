using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Schoolozor.Http;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel;
using Schoolozor.Repository;
using Schoolozor.Shared.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Schoolozor.Services
{
    public interface IClientUserService
    {
        event EventHandler<UserAuthenticatedArgs> UserAuthenticatedEvent;
        Task<LoginResult> LoginUser(Credentials user);
        Task LogoutUser();
        Task<LoginResult> RegisterUser(UserRegistration user);
        Task<SchoolUser> GetUserInfo(string Id);
        Task<IEnumerable<SchoolUser>> GetUsers();
    }

    public class ClientUserService : IClientUserService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly AuthenticationStateProvider _authProvider;
        private readonly string ApiUrl;

        public event EventHandler<UserAuthenticatedArgs> UserAuthenticatedEvent;
        public ClientUserService(IGenericRepository genericRepository,
            AuthenticationStateProvider authProvider,
            IConfiguration config)
        {
            _genericRepository = genericRepository;
            _authProvider = authProvider;
            _authProvider.AuthenticationStateChanged += _authProvider_AuthenticationStateChanged;
            ApiUrl = config.GetSection("ApiUrl").Value;
        }

        private void _authProvider_AuthenticationStateChanged(Task<Microsoft.AspNetCore.Components.Authorization.AuthenticationState> task)
        {
            if (task.Result.User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)task.Result.User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                UserAuthenticatedEvent?.Invoke(this, new UserAuthenticatedArgs(userId));
            }
            else
            {
                UserAuthenticatedEvent?.Invoke(this, new UserAuthenticatedArgs(""));
            }
        }

        public async Task<LoginResult> LoginUser(Credentials user)
        {
            try
            {
                ResponseResult<LoginResult> lr = await _genericRepository.PostAsync<Credentials, ResponseResult<LoginResult>>("/api/v1/security/login", user);
                if (lr.Succeed)
                {
                    await ((ISchoolAuthenticationStateProvider)_authProvider).MarkUserAsAuthenticated(lr.Data);
                    return lr.Data;
                }
                else {
                    Log.Error(lr.Error.Description);
                    await LogoutUser();
                    return new LoginResult();
                }
            }
            catch (HttpRequestExceptionEx e)
            {
                Log.Error(e, e.Message);
                await LogoutUser();
                return new LoginResult();
            }
        }
        public async Task LogoutUser()
        {
            await ((ISchoolAuthenticationStateProvider)_authProvider).MarkUserAsLoggedOut();
        }
        public async Task<LoginResult> RegisterUser(UserRegistration user)
        {
            try
            {
                LoginResult lr = await _genericRepository.PostAsync<UserRegistration, LoginResult>("api/register", user);
                await ((ISchoolAuthenticationStateProvider)_authProvider).MarkUserAsAuthenticated(lr);
                return lr;
            }
            catch (HttpRequestExceptionEx e)
            {
                Log.Error(e, e.Message);
                await LogoutUser();
                return new LoginResult();
            }
        }
        public async Task<IEnumerable<SchoolUser>> GetUsers()
        {
            try
            {
                return await _genericRepository.GetAsync<IEnumerable<SchoolUser>>("api/users");
            }
            catch (HttpRequestExceptionEx e)
            {
                Log.Error(e, e.Message);
                await LogoutUser();
                return new List<SchoolUser>();
            }

        }
        public async Task<SchoolUser> GetUserInfo(string Id)
        {
            try
            {
                return await _genericRepository.GetAsync<SchoolUser>($"api/user/{Id}");

            }
            catch (HttpRequestExceptionEx e)
            {
                Log.Error(e, e.Message);
                await LogoutUser();
                return new SchoolUser();
            }
        }
    }
}
