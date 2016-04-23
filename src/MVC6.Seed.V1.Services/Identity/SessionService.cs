using Microsoft.AspNet.Identity;
using MVC6.Seed.V1.Framework.Models.Identity;
using MVC6.Seed.V1.Services.Identity.Exceptions;
using MVC6.Seed.V1.Services.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.Framework.Services;
using MVC6.Seed.V1.Framework.Utility;
using Microsoft.Data.Entity;

namespace MVC6.Seed.V1.Services.Identity
{
    public interface ISessionService
    {
        Task Login(LoginRequest request);

        Task Logout();
    }

    public class SessionService : ISessionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityResolver _identityResolver;

        public SessionService(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityResolver identityResolver)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _identityResolver = identityResolver;
        }

        public async Task Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new IdentityErrorException("Invalid login attempt.");
            }

            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isEmailConfirmed)
            {
                throw new IdentityErrorException("You must have a confirmed email to log in.");
            }

            var result = await _signInManager.PasswordSignInAsync(
                request.Email, request.Password, request.RememberMe, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                throw new IdentityErrorException("User account locked out.");
            }
            else if (!result.Succeeded)
            {
                throw new IdentityErrorException("Invalid login attempt.");
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
