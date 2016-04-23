using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using MVC6.Seed.V1.Framework.Models.Identity;
using MVC6.Seed.V1.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Services
{
    public static class ServiceCollectionExtensions
    {

        public static void AddServices(
            this IServiceCollection services,
            string connectionString,
            string loginPath = null)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                if (loginPath != null)
                {
                    options.Cookies.ApplicationCookie.LoginPath = loginPath;
                }
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Identity
            services.AddScoped<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<ICurrentUserProvider, CurrentUserProvider>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IRoleResolver, RoleResolver>();
            services.AddScoped<IAuditFactory, AuditFactory>();
        }
    }
}
