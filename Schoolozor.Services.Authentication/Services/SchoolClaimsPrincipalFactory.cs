using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Schoolozor.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Schoolozor.Services.Authentication.Services
{
    public class SchoolClaimsPrincipalFactory : UserClaimsPrincipalFactory<SchoolUser, IdentityRole>
    {
        public SchoolClaimsPrincipalFactory(
            UserManager<SchoolUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> option) : base(userManager, roleManager, option)
        {

        }

        public async override Task<ClaimsPrincipal> CreateAsync(SchoolUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim(ClaimTypes.GivenName, user.FirstName)
                });
            }
            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim(ClaimTypes.Surname, user.LastName)
                });
            }
            return principal;
        }
    }
}
