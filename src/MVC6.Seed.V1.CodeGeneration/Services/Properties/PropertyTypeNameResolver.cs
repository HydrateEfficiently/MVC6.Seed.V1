using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MVC6.Seed.V1.Framework.Models.Identity;

namespace MVC6.Seed.V1.CodeGeneration.Services.Properties
{
    public interface IPropertyTypeNameResolver
    {
        string Resolve(PropertyInfo pi);
    }

    public class PropertyTypeNameResolver : IPropertyTypeNameResolver
    {
        private static readonly Dictionary<Type, string> __netTypeMappings =
            new Dictionary<Type, string>()
            {
                { typeof(string), "string" },
                { typeof(int), "int" },
                { typeof(bool), "bool" }
            };

        public string Resolve(PropertyInfo pi)
        {
            var type = pi.PropertyType;
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return $"{GetNonNullableTypeName(underlyingType)}?";
            }
            return GetNonNullableTypeName(type);
        }

        #region Helpers

        private string GetNonNullableTypeName(Type type)
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
