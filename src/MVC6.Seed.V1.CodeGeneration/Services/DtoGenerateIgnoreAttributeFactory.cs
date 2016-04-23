using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Core;

namespace MVC6.Seed.V1.CodeGeneration.Services
{
    public interface IDtoGenerateIgnoreAttributeFactory
    {
        string CreateAttributeString(params DtoType[] dtoTypes);
    }

    public class DtoGenerateIgnoreAttributeFactory : IDtoGenerateIgnoreAttributeFactory
    {
        private const string AttributeSuffix = "Attribute";

        public string CreateAttributeString(params DtoType[] dtoTypes)
        {
            var attributeName = nameof(DtoGenerateIgnoreAttribute);
            if (dtoTypes.Contains(DtoType.Unknown))
            {
                throw new Exception($"Unknown is not a valid DtoType to add to {attributeName}");
            }

            if (dtoTypes.Length == 0)
            {
                throw new Exception($"Must include at least one DtoType to add to {attributeName}");
            }

            var resolvedDtoTypes = dtoTypes.Distinct().OrderBy(t => t);
            var shortAttributeName = attributeName.Substring(0, attributeName.Length - AttributeSuffix.Length);
            var dtoTypeParams = resolvedDtoTypes.Select(t => $"{nameof(DtoType)}.{t}");
            return $"[{shortAttributeName}({string.Join(", ", dtoTypeParams)})]";
        }
    }
}
