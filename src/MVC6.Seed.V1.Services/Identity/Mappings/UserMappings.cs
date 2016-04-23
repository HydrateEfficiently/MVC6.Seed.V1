using AutoMapper;
using MVC6.Seed.V1.Framework.Models.Identity;
using MVC6.Seed.V1.Services.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Services.Identity.Mappings
{
    public static class UserMappings
    {
        public static IMappingExpression<TSource, TDestination> ForUser<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> self,
            Expression<Func<TDestination, object>> destinationMember,
            Func<TSource, ApplicationUser> source)
        {
            return self.ForMember(
                destinationMember,
                options => options.MapFrom(src =>
                    source(src) == null ? null : new UserResult(source(src))
                ));
        }
    }
}
