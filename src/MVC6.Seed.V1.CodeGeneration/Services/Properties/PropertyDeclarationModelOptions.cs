using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Services.Properties
{
    public class PropertyDeclarationModelOptions
    {
        public string AccessModifier { get; internal set; } = "public";
        public bool IsVirtual { get; internal set; } = false;
        public IEnumerable<string> Attributes { get; internal set; } = Enumerable.Empty<string>();
        public bool HasAttribute { get { return Attributes.Count() > 0; } }
    }
}
