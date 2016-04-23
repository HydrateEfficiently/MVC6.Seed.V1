using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Services
{
    public class NamespaceService
    {
        private readonly string _rootNamespace;

        public NamespaceService(
            ILibraryManager libraryManager,
            IApplicationEnvironment appEnvironment)
        {
            string appName = libraryManager.GetLibrary(appEnvironment.ApplicationName).Name;
            _rootNamespace = appName.Split('.')[0];
        }

        public string GetRootNamespace()
        {
            return _rootNamespace;
        }

        public string GetFrameworkNamespace()
        {
            return $"{GetRootNamespace()}.Framework";
        }

        public string GetFrameworkEntitiesNamespace()
        {
            return $"{GetFrameworkNamespace()}.Models";
        }

        public string GetFrameworkEntityNamespace(string areaName)
        {
            return $"{GetFrameworkEntitiesNamespace()}.{areaName}";
        }

        public string GetFrameworkUtilityNamespace()
        {
            return $"{GetFrameworkNamespace()}.Utility";
        }

        public string GetFrameworkServicesNamespace()
        {
            return $"{GetFrameworkNamespace()}.Services";
        }

        public string GetServicesNamespace()
        {
            return $"{GetRootNamespace()}.Services";
        }

        public string GetServiceNamespace(string areaName)
        {
            return $"{GetServicesNamespace()}.{areaName}";
        }

        public string GetServiceModelsNamespace(string areaName)
        {
            return $"{GetServiceNamespace(areaName)}.Models";
        }

        public string GetServiceMappingsNamespace(string areaName)
        {
            return $"{GetServiceNamespace(areaName)}.Mappings";
        }

        public string GetWebNamespace()
        {
            return $"{GetRootNamespace()}.Web";
        }

        public string GetWebControllersNamespace()
        {
            return $"{GetWebNamespace()}.Controllers";
        }

        public string GetWebApiControllersNamespace()
        {
            return $"{GetWebControllersNamespace()}.Api";
        }

        public string GetWebApiControllerNamespace(string areaName)
        {
            return $"{GetWebApiControllersNamespace()}.{areaName}";
        }

        public string GetGenerationCoreNamespace()
        {
            return $"{GetRootNamespace()}.CodeGeneration.Core";
        }
    }
}
