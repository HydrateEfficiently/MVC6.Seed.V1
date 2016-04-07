using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using MVC6.Seed.V1.Framework.Constants;
using MVC6.Seed.V1.Framework.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Services.Identity
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {

        public ApplicationSignInManager(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<ApplicationUser>> logger)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
        }

        public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(ApplicationUser user)
        {
            var principal = await base.CreateUserPrincipalAsync(user);

            var identity = principal.Identity as ClaimsIdentity;
            if (identity != null)
            {
                identity.AddClaim(new Claim(ApplicationClaimTypes.Id, user.Id));
                identity.AddClaim(new Claim(ApplicationClaimTypes.Email, user.Email));
                identity.AddClaim(new Claim(ApplicationClaimTypes.UserName, user.UserName));
            }

            return principal;
        }
    }
}
