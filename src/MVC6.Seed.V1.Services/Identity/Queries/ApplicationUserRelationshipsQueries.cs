using MVC6.Seed.V1.Framework.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Services.Identity.Queries
{
    public static class ApplicationUserRelationshipsQueries
    {
        public static IQueryable<ApplicationUserRelationships> QueryByUser(
            this IQueryable<ApplicationUserRelationships> queryable,
            string userId)
        {
            return queryable.Where(r => r.UserId == userId);
        }
    }
}
