using Schoolozor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolozor.Services
{
    public interface ISchoolAuthenticationStateProvider
    {
        Task MarkUserAsAuthenticated(LoginResult loginResult);
        Task MarkUserAsLoggedOut();
    }
}
