using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Services
{
    public interface IAssemblyProvider
    {
        Assembly GetAssembly(LocalProjectType project);

        Assembly GetFrameworkAssembly();

        Assembly GetServicesAssembly();

        Assembly GetWebAssembly();
    }

    public class AssemblyProvider : IAssemblyProvider
    {
        private readonly NamespaceService _namespaceService;

        public AssemblyProvider(
            NamespaceService namespaceService)
        {
            _namespaceService = namespaceService;

            // Force load of projects
            MVC6.Seed.V1.Services.Startup loadServices;
            MVC6.Seed.V1.Framework.Models.Identity.ApplicationUser loadFramework;
            MVC6.Seed.V1.Web.Startup loadWeb;
        }

        public Assembly GetAssembly(LocalProjectType project)
        {
            switch (project)
            {
                case LocalProjectType.Framework:
                    return GetFrameworkAssembly();
                case LocalProjectType.Services:
                    return GetServicesAssembly();
                case LocalProjectType.Web:
                    return GetWebAssembly();
                default:
                    throw new InvalidOperationException("Unrecognised local project type");
            }
        }

        public Assembly GetFrameworkAssembly()
        {
            return GetAssembly(_namespaceService.GetFrameworkNamespace());
        }

        public Assembly GetServicesAssembly()
        {
            return GetAssembly(_namespaceService.GetServicesNamespace());
        }

        public Assembly GetWebAssembly()
        {
            return GetAssembly(_namespaceService.GetWebNamespace());
        }

        #region Helpers

        private Assembly GetAssembly(string namespaceName)
        {
            var namespaceAssemblyName = Assembly
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Where(a => a.Name == namespaceName)
                .FirstOrDefault();

            if (namespaceAssemblyName == null)
            {
                throw new InvalidOperationException($"Could not find assembly with name {namespaceName}");
            }

            return Assembly.Load(namespaceName);
        }

        #endregion
    }
}
