using Microsoft.AspNet.Identity;
using MVC6.Seed.V1.Framework.Models.Identity;
using MVC6.Seed.V1.Framework.Services;
using MVC6.Seed.V1.Framework.Utility;
using MVC6.Seed.V1.Services.Identity.Models;
using MVC6.Seed.V1.Services.Identity.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Services.Identity
{
    public interface ICurrentUserProvider
    {
        Task<ApplicationUser> GetApplicationUserAsync();

        Task<UserResult> GetUserResultAsync();
    }

    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IIdentityResolver _identityResolver;
        private readonly ApplicationDbContext _dbContext;

        public CurrentUserProvider(
            IIdentityResolver identityResolver,
            ApplicationDbContext dbContext)
        {
            _identityResolver = identityResolver;
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser> GetApplicationUserAsync()
        {
            var identity = _identityResolver.Resolve();
            return await _dbContext.UserRelationships.GetUserAsync(identity.GetId());
        }

        public async Task<UserResult> GetUserResultAsync()
        {
            return new UserResult(await GetApplicationUserAsync());
        }
    }
}
