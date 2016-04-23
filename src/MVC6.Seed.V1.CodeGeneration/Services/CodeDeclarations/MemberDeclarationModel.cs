using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Utility;

namespace MVC6.Seed.V1.CodeGeneration.Services.CodeDeclarations
{
    public class MemberDeclarationModel
    {
        public string AccessModifier { get; internal set; } = "private";
        public bool IsReadOnly { get; internal set; } = false;
        public bool IsTypeInterface { get; internal set; } = false;
        public string TypeName { get; internal set; }

        public string ArgumentName
        {
            get
            {
                string result = TypeName;
                if (IsTypeInterface)
                {
                    result = result.Substring(1); // Strip off the 'I' for Interface;
                }
                return result.ToFirstCharacterLower();
            }
        }

        public string LocalName
        {
            get
            {
                return $"_{ArgumentName}";
            }
        }
    }
}
