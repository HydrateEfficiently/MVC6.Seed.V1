using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Services
{
    public class OutputClassNameResolver
    {
        public string GetServiceName(string entityName)
        {
            return $"{entityName}Service";
        }

        public string GetReadDtoName(string entityName)
        {
            return $"{entityName}Result";
        }

        public string GetCreateDtoName(string entityName)
        {
            return $"{entityName}Create";
        }

        public string GetUpdateEntityDtoName(string entityName)
        {
            return $"{entityName}Update";
        }
    }
}
