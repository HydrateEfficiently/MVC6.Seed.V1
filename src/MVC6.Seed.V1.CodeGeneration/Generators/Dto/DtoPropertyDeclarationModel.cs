using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Services.Properties;

namespace MVC6.Seed.V1.CodeGeneration.Generators.Dto
{
    public class DtoPropertyDeclarationModel : PropertyDeclarationModel
    {
        public string BasePropertyTypeName { get; internal set; }
    }
}
