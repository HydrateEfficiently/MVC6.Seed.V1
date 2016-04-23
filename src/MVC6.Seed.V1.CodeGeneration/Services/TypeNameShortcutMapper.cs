using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Services
{
    public interface ITypeNameShortcutMapper
    {
        string Map(string input);
    }

    public class TypeNameShortcutMapper : ITypeNameShortcutMapper
    {
        public string Map(string input)
        {
            switch (input)
            {
                case "s":
                    return "string";
                case "g":
                    return "Guid";
                case "i":
                    return "int";
                case "d":
                    return "double";
                case "dt":
                    return "DateTime";
                case "ts":
                    return "TimeSpan";
                default:
                    throw new InvalidOperationException($"Could not find a property type to map from {input}");
            }
        }
    }
}
