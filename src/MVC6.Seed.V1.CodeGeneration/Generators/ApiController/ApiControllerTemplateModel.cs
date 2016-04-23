using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Services.CodeDeclarations;

namespace MVC6.Seed.V1.CodeGeneration.Generators.ApiController
{
    public class ApiControllerTemplateModel
    {
        public IEnumerable<ActionDeclarationModel> Actions { get; internal set; }
        public string ClassName { get; internal set; }
        public List<MemberDeclarationModel> InjectedServices { get; internal set; }
        public string NamespaceName { get; internal set; }
        public MemberDeclarationModel Service { get; internal set; }
        public IEnumerable<string> UsingNamespaceNames { get; internal set; }
    }
}
