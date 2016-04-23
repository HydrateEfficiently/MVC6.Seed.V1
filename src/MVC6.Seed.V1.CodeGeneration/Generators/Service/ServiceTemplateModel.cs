using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Generators.Service
{
    public class ServiceTemplateModel
    {
        public string AreaName { get; internal set; }
        public bool CanDelete { get; internal set; }
        public bool CanList { get; internal set; }
        public bool CanUpdate { get; internal set; }
        public string CreateEntityDtoName { get; internal set; }
        public string DbSetPropertyName { get; internal set; }
        public string EntitiesNamespaceName { get; internal set; }
        public string EntityIdTypeName { get; internal set; }
        public string EntityName { get; internal set; }
        public string EntityNamespaceName { get; internal set; }
        public string FrameworkServicesNamespaceName { get; internal set; }
        public string ReadEntityDtoName { get; internal set; }
        public string ServiceModelsNamespaceName { get; internal set; }
        public string ServiceName { get; internal set; }
        public string ServiceNamespaceName { get; internal set; }
        public string ServicesNamespaceName { get; internal set; }
        public string UpdateEntityDtoName { get; internal set; }
    }
}
