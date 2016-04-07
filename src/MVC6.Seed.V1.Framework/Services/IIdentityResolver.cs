using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Framework.Services
{
    public interface IIdentityResolver
    {
        ClaimsPrincipal Resolve();

        bool IsSignedIn();
    }
}
