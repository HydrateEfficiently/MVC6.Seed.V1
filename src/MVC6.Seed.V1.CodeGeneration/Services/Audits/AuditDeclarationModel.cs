using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Services.Properties;

namespace MVC6.Seed.V1.CodeGeneration.Services.Audits
{
    public class AuditDeclarationModel
    {
        public string FromInterface { get; internal set; }
        public string Name { get; internal set; }
        public bool IsNullable { get; internal set; }
        public bool IsFromInterface { get { return !string.IsNullOrEmpty(FromInterface); } }
    }
}
