using MVC6.Seed.V1.Framework.Constants;
using MVC6.Seed.V1.Framework.Services;
using MVC6.Seed.V1.Initializer.Initializers.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Initializer.Services
{
    public class SystemUserIdentityResolver : IIdentityResolver
    {

        private readonly SystemUserInitializer _systemUserInitializer;

        public SystemUserIdentityResolver(SystemUserInitializer systemUserInitializer)
        {
            _systemUserInitializer = systemUserInitializer;
        }

        public bool IsSignedIn()
        {
            return true;
        }

        public ClaimsPrincipal Resolve()
        {
            var systemUser = _systemUserInitializer.GetUser();
            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ApplicationClaimTypes.Id, systemUser.Id),
                new Claim(ApplicationClaimTypes.Email, systemUser.Email),
                new Claim(ApplicationClaimTypes.UserName, systemUser.UserName)
            }));
            return principal;
        }
    }
}
