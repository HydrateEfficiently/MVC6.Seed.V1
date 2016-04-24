using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Utility;

namespace MVC6.Seed.V1.CodeGeneration.Services
{
    public interface IServiceReflector
    {
        Type GetServiceType(string serviceName);

        string GetAreaName(string serviceName);
    }

    public class ServiceReflector : IServiceReflector
    {
        private readonly NamespaceService _namespaceService;
        private readonly IAssemblyProvider _assemblyProvider;

        private Dictionary<string, Type> _serviceTypesByName = new Dictionary<string, Type>();
        private Dictionary<string, string> _areaNamesByByServiceName = new Dictionary<string, string>();

        public ServiceReflector(
            NamespaceService namespaceService,
            IAssemblyProvider assemblyProvider)
        {
            _namespaceService = namespaceService;
            _assemblyProvider = assemblyProvider;
        }

        public Type GetServiceType(string serviceName)
        {
            Type serviceType;
            if (!_serviceTypesByName.TryGetValue(serviceName, out serviceType))
            {
                var frameworkAssembly = _assemblyProvider.GetServicesAssembly();
                serviceType = frameworkAssembly.GetTypeAndValidate(serviceName);
                _serviceTypesByName.Add(serviceName, serviceType);
            }
            return serviceType;
        }

        public string GetAreaName(string serviceName)
        {
            string areaName;
            if (!_areaNamesByByServiceName.TryGetValue(serviceName, out areaName))
            {
                string servicesNamespaceName = _namespaceService.GetServicesNamespace();
                string serviceFullName = GetServiceType(serviceName).FullName;
                string serviceRelativeFullName = serviceFullName.Replace(servicesNamespaceName + ".", string.Empty);
                var serviceNameParts = serviceRelativeFullName.Split('.');

                if (serviceNameParts.Count() != 2)
                {
                    throw new InvalidOperationException($"Service namespace must be one below {servicesNamespaceName}");
                }

                areaName = serviceNameParts.First();
                _areaNamesByByServiceName.Add(serviceName, areaName);
            }
            return areaName;
        }
    }
}
