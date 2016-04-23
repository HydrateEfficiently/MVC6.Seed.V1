using Microsoft.Data.Entity;
using MVC6.Seed.V1.Framework.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Services.Identity.Queries
{
    public static class ApplicationUserQueries
    {
        public static async Task<ApplicationUser> GetUserAsync(
            this IQueryable<ApplicationUserRelationships> queryable,
            string userId)
        {
            return await queryable
                .QueryByUser(userId)
                .Select(r => r.User)
                .FirstAsync();
        }
    }
}
