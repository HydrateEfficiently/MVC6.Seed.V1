using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Services.CodeDeclarations;

namespace MVC6.Seed.V1.CodeGeneration.Generators.ApiController
{
    public class ActionDeclarationModel
    {
        public IEnumerable<string> Attributes { get; internal set; } = Enumerable.Empty<string>();
        public MethodDeclarationModel Method { get; internal set; }
        public ServiceMethodInvocationModel ServiceMethod { get; internal set; }
        
        public bool IsAsync
        {
            get
            {
                return ServiceMethod.IsAsync;
            }
        }
    }
}
