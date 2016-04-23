using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Services.Properties;
using MVC6.Seed.V1.CodeGeneration.Utility;

namespace MVC6.Seed.V1.CodeGeneration.Generators.Dto
{
    public class DtoTemplateModel
    {
        public string EntityName { get; internal set; }
        public string EntityNamespaceName { get; internal set; }
        public string DtoName { get; internal set; }
        public string NamespaceName { get; internal set; }
        public List<PropertyDeclarationModel> Properties { get; internal set; }
        public List<DtoPropertyDeclarationModel> DtoProperties { get; internal set; }
        public List<string> DtoNamespaceNames { get; internal set; }
    }
}
