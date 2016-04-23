using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Services
{
    public class OutputPathResolver
    {
        private readonly NamespaceService _namespaceService;
        private readonly IApplicationEnvironment _applicationEnvironment;

        public OutputPathResolver(
            NamespaceService namespaceService,
            IApplicationEnvironment applicationEnvironment)
        {
            _namespaceService = namespaceService;
            _applicationEnvironment = applicationEnvironment;
        }

        public string GetFrameworkEntityNamespace(string areaName)
        {
            return GetPath(_namespaceService.GetFrameworkEntityNamespace(areaName));
        }

        public string GetServicesPath()
        {
            return GetPath(_namespaceService.GetServicesNamespace());
        }

        public string GetServicePath(string areaName)
        {
            return GetPath(_namespaceService.GetServiceNamespace(areaName));
        }

        public string GetServiceModelsPath(string areaName)
        {
            return GetPath(_namespaceService.GetServiceModelsNamespace(areaName));
        }

        public string GetServiceMappingsPath(string areaName)
        {
            return GetPath(_namespaceService.GetServiceMappingsNamespace(areaName));
        }

        public string GetWebApiControllerPath(string areaName)
        {
            return GetPath(_namespaceService.GetWebApiControllerNamespace(areaName));
        }

        #region Helpers

        private string GetSrcPath()
        {
            return Directory.GetParent(_applicationEnvironment.ApplicationBasePath).FullName;
        }

        private string GetPath(string pathNamespace)
        {
            string rootNamespace = _namespaceService.GetRootNamespace();
            var namespaceSplit = pathNamespace.Split('.').ToList();
            var projectDirectory = string.Join(".", namespaceSplit.Take(2).ToArray());
            var pathFromProjectDirectory = namespaceSplit.Skip(2);
            return Path.Combine(GetSrcPath(), projectDirectory, Path.Combine(pathFromProjectDirectory.ToArray()));
        }

        #endregion
    }
}
