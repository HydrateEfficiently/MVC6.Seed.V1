using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC6.Seed.V1.Framework.Models.Identity;
using Microsoft.Data.Entity;

namespace MVC6.Seed.V1.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        //public DbSet<Foo> Foos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Foo>().HasTableName(nameof(Foos));
        }
    }
}
