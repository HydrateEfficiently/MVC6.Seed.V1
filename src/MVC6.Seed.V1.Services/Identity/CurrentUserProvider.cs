using Microsoft.AspNet.Identity;
using MVC6.Seed.V1.Framework.Models.Identity;
using MVC6.Seed.V1.Framework.Services;
using MVC6.Seed.V1.Framework.Utility;
using MVC6.Seed.V1.Services.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Services.Identity
{
    public interface ICurrentUserProvider
    {
        Task<UserSummary> GetAsync();
    }

    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IIdentityResolver _identityResolver;
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentUserProvider(
            IIdentityResolver identityResolver,
            UserManager<ApplicationUser> userManager)
        {
            _identityResolver = identityResolver;
            _userManager = userManager;
        }

        public async Task<UserSummary> GetAsync()
        {
            var identity = _identityResolver.Resolve();
            var user = await _userManager.FindByIdAsync(identity.GetId());
            return new UserSummary(user);
        }
    }
}
