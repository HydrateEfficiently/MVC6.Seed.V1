using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Core;
using MVC6.Seed.V1.CodeGeneration.Exceptions;
using MVC6.Seed.V1.CodeGeneration.Generators.Entity;
using MVC6.Seed.V1.CodeGeneration.Services;
using MVC6.Seed.V1.CodeGeneration.Services.Audits;
using MVC6.Seed.V1.CodeGeneration.Services.Properties;

namespace MVC6.Seed.V1.CodeGeneration.Generators.DataContext
{
    public class DataContextGenerator : IGenerator
    {
        private readonly ScaffoldingService _scaffoldingService;
        private readonly OutputPathResolver _outputPathResolver;
        private readonly NamespaceService _namespaceService;
        private readonly IAssemblyProvider _assemblyProvider;
        private readonly DataContextCommandLineModel _model;

        public DataContextGenerator(
            ScaffoldingService scaffoldingService,
            OutputPathResolver outputPathResolver,
            NamespaceService namespaceService,
            IAssemblyProvider assemblyProvider,
            DataContextCommandLineModel model)
        {
            _scaffoldingService = scaffoldingService;
            _outputPathResolver = outputPathResolver;
            _namespaceService = namespaceService;
            _assemblyProvider = assemblyProvider;
            _model = model;
        }

        public async Task Generate()
        {
            var generatedEntities = _assemblyProvider.GetFrameworkAssembly().GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(GeneratedEntityAttribute), false).Length > 0);

            var entityNamespaces = generatedEntities
                .Select(t => t.Namespace)
                .Distinct();
            var identityEntitiesNamespace = _namespaceService.GetFrameworkEntityNamespace("Identity");
            if (!entityNamespaces.Contains(identityEntitiesNamespace))
            {
                var entityNamespacesList = entityNamespaces.ToList();
                entityNamespacesList.Add(identityEntitiesNamespace);
                entityNamespaces = entityNamespacesList;

            }
            entityNamespaces = entityNamespaces.OrderBy(s => s);

            var entities = generatedEntities.Select(t => new EntityDeclarationModel()
            {
                TableName = t.GetCustomAttributes(typeof(GeneratedEntityAttribute), false)
                    .Cast<GeneratedEntityAttribute>()
                    .Select(a => a.TableName)
                    .First(),
                TypeName = t.Name
            }).OrderBy(e => e.TypeName);

            var utilityNamespace = _namespaceService.GetFrameworkUtilityNamespace();

            var servicesNamespace = _namespaceService.GetServicesNamespace();

            var templateModel = new DataContextGeneratedTemplateModel()
            {
                UtilityNamespace = _namespaceService.GetFrameworkUtilityNamespace(),
                ServicesNamespace = _namespaceService.GetServicesNamespace(),
                EntityNamespaces = entityNamespaces,
                GeneratedEntities = entities
            };

            try
            {
                await _scaffoldingService.ScaffoldAsync(
                    _outputPathResolver.GetServicesPath(),
                    "ApplicationDbContext",
                    "DataContextTemplate",
                    templateModel as DataContextTemplateModel,
                    force: _model.Force);
            }
            catch (GeneratedFileExistsException)
            {
            }

            await _scaffoldingService.ScaffoldAsync(
                _outputPathResolver.GetServicesPath(),
                "ApplicationDbContext.Generated",
                "DataContextGeneratedTemplate",
                templateModel,
                force: true);
        }
    }
}
