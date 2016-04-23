using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Utility
{
    public static class AssemblyExtensions
    {
        public static Type GetTypeAndValidate(
            this Assembly assembly,
            string typeName)
        {
            var candidateTypes = assembly.GetTypes().Where(t => t.Name == typeName);
            if (candidateTypes.Count() == 0)
            {
                throw new InvalidOperationException(
                    $"Could not find type with name, {typeName}");
            }
            else if (candidateTypes.Count() > 1)
            {
                throw new InvalidOperationException(
                    $"Found multiple types with name {typeName}: ${string.Join(", ", candidateTypes.Select(e => e.FullName))}");
            }
            return candidateTypes.Single();
        }
    }
}
