using AutoMapper;
using MVC6.Seed.V1.Framework.Models.Identity;
using MVC6.Seed.V1.Services.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Services.Utility
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> ForMemberResolveUsing<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpr,
            Expression<Func<TDestination, object>> destinationMember,
            Func<TSource, object> resolver)
        {
            return mappingExpr.ForMember(destinationMember, opts => opts.ResolveUsing(resolver));
        }

        public static IMappingExpression<TSource, TDestination> ForMemberIgnore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpr,
            Expression<Func<TDestination, object>> destinationMember)
        {
            return mappingExpr.ForMember(destinationMember, opts => opts.Ignore());
        }
    }
}
