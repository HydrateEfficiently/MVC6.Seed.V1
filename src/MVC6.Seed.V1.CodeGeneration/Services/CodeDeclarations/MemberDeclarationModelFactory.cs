using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Services.CodeDeclarations
{
    public interface IMemberDeclarationModelFactory
    {
        MemberDeclarationModel CreateInjectedService(Type service);
    }

    public class MemberDeclarationModelFactory : IMemberDeclarationModelFactory
    {
        public MemberDeclarationModel CreateInjectedService(Type serviceType)
        {
            return new MemberDeclarationModel()
            {
                AccessModifier = "private",
                IsTypeInterface = serviceType.IsInterface,
                IsReadOnly = true,
                TypeName = serviceType.Name
            };
        }
    }
}
