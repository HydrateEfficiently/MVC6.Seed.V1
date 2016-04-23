using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Generators.DtoMappings
{
    public class DtoMappingsTemplateModel
    {
        public string ClassName { get; internal set; }
        public string CreateDtoName { get; internal set; }
        public object EntityName { get; internal set; }
        public string EntityNamespaceName { get; internal set; }
        public string ModelsNamespaceName { get; internal set; }
        public string NamespaceName { get; internal set; }
        public string ReadDtoName { get; internal set; }
    }
}
