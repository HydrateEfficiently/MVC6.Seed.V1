using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Utility
{
    public static class TypeExtensions
    {
        public static bool IsPropertyNullable(
            this Type type,
            string propertyName)
        {
            var propertyType = type.GetProperty(propertyName).PropertyType;
            return Nullable.GetUnderlyingType(propertyType) != null;
        }

    }
}
