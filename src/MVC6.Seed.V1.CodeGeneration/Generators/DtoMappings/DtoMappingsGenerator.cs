using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Commands.Models;
using MVC6.Seed.V1.CodeGeneration.Core;
using MVC6.Seed.V1.CodeGeneration.Services;

namespace MVC6.Seed.V1.CodeGeneration.Generators.DtoMappings
{
    public class DtoMappingsGenerator : IGenerator
    {
        private readonly IEntityAliasResolver _entityAliasResolver;
        private readonly IEntityReflector _entityReflector;
        private readonly IDtoAliasResolver _dtoAliasResolver;
        private readonly NamespaceService _namespaceService;
        private readonly OutputPathResolver _outputPathResolver;
        private readonly ScaffoldingService _scaffoldingService;
        private readonly FromEntityCommandLineModel _model;

        public DtoMappingsGenerator(
            IEntityAliasResolver entityAliasResolver,
            IEntityReflector entityReflector,
            IDtoAliasResolver dtoAliasResolver,
            NamespaceService namespaceService,
            OutputPathResolver outputPathResolver,
            ScaffoldingService scaffoldingService,
            FromEntityCommandLineModel model)
        {
            _entityAliasResolver = entityAliasResolver;
            _entityReflector = entityReflector;
            _dtoAliasResolver = dtoAliasResolver;
            _namespaceService = namespaceService;
            _outputPathResolver = outputPathResolver;
            _scaffoldingService = scaffoldingService;
            _model = model;
        }

        public async Task Generate()
        {
            var entityName = _model.Entity;
            var areaName = _entityReflector.GetAreaName(entityName);
            var aliasedEntityName = _entityAliasResolver.Resolve(entityName);
            var className = $"{aliasedEntityName}Mappings";

            var templateModel = new DtoMappingsTemplateModel()
            {
                ClassName = className,
                NamespaceName = _namespaceService.GetServiceMappingsNamespace(areaName),
                EntityName = entityName,
                EntityNamespaceName = _namespaceService.GetFrameworkEntityNamespace(areaName),
                ModelsNamespaceName = _namespaceService.GetServiceModelsNamespace(areaName),
                ReadDtoName = _dtoAliasResolver.Resolve(aliasedEntityName, DtoType.Read),
                CreateDtoName = _dtoAliasResolver.Resolve(aliasedEntityName, DtoType.Create)
            };

            await _scaffoldingService.ScaffoldAsync(
                _outputPathResolver.GetServiceMappingsPath(areaName),
                className,
                "DtoMappingsTemplate",
                templateModel,
                _model.Force);
        }
    }
}
