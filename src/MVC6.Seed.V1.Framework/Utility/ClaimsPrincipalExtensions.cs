using MVC6.Seed.V1.Framework.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Framework.Utility
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ApplicationClaimTypes.Id);
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ApplicationClaimTypes.Email);
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ApplicationClaimTypes.UserName);
        }
    }
}
