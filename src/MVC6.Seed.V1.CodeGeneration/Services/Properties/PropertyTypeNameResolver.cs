using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Utility;
using MVC6.Seed.V1.Framework.Models.Identity;

namespace MVC6.Seed.V1.CodeGeneration.Services.Properties
{
    public interface IPropertyTypeNameResolver
    {
        string Resolve(PropertyInfo pi);
    }

    public class PropertyTypeNameResolver : IPropertyTypeNameResolver
    {
        public string Resolve(PropertyInfo pi)
        {
            return pi.PropertyType.GetRawOutputName();
        }
    }
}
