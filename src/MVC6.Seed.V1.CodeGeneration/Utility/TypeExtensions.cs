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

        public static string GetRawOutputName(
            this Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return $"{GetNonNullableTypeName(underlyingType)}?";
            }
            return GetNonNullableTypeName(type);
        }

        #region Helpers

        private static readonly Dictionary<Type, string> __netTypeMappings =
            new Dictionary<Type, string>()
            {
                { typeof(string), "string" },
                { typeof(int), "int" },
                { typeof(bool), "bool" }
            };

        private static string GetNonNullableTypeName(Type type)
        {
            string result;
            if (__netTypeMappings.TryGetValue(type, out result)) { }
            else
            {
                result = type.Name;
            }
            return result;
        }

        #endregion

    }
}
