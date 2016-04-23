using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC6.Seed.V1.Framework.Models.Identity;
using Microsoft.Data.Entity;
using MVC6.Seed.V1.Framework.Utility;

namespace MVC6.Seed.V1.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<ApplicationUserRelationships> UserRelationships { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRelationships>().HasTableName(nameof(UserRelationships));
        }
    }
}
