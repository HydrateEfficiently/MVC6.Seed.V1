using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Services.Properties;

namespace MVC6.Seed.V1.CodeGeneration.Utility
{
    public class EntityPropertyDeclarationModel : PropertyDeclarationModel
    {
        public bool IsAuditProperty { get; set; }
    }
}
