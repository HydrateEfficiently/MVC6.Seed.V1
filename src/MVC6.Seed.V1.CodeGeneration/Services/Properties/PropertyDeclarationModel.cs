using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Services.Properties
{
    public class PropertyDeclarationModel
    {
        public string AccessModifier { get; internal set; }
        public string PropertyName { get; internal set; }
        public string PropertyTypeName { get; internal set; }
        public string ExtraModifiers { get; internal set; }
        public bool HasExtraModifier { get { return !string.IsNullOrEmpty(ExtraModifiers); } }
        public IEnumerable<string> Attributes { get; internal set; } = Enumerable.Empty<string>();
        public bool HasAttribute { get { return Attributes.Count() > 0; } }
    }
}
