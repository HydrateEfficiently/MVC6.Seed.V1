using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Services;

namespace MVC6.Seed.V1.CodeGeneration.Generators.Service
{
    public class ServiceGenerator : IGenerator
    {
        private const string ServiceSuffix = "Service";

        private readonly NamespaceService _namespaceService;
        private readonly IEntityReflector _entityReflector;
        private readonly IEntityOperationsResolver _entityOperationsResolver;
        private readonly OutputClassNameResolver _outputClassNameResolver;
        private readonly OutputPathResolver _outputPathResolver;
        private readonly ScaffoldingService _scaffoldingService;
        private readonly ServiceCommandLineModel _model;

        public ServiceGenerator(
            NamespaceService namespaceService,
            IEntityReflector entityReflector,
            IEntityOperationsResolver entityOperationsResolver,
            OutputClassNameResolver outputClassNameResolver,
            OutputPathResolver outputPathResolver,
            ScaffoldingService scaffoldingService,
            ServiceCommandLineModel model)
        {
            _namespaceService = namespaceService;
            _entityReflector = entityReflector;
            _entityOperationsResolver = entityOperationsResolver;
            _outputClassNameResolver = outputClassNameResolver;
            _outputPathResolver = outputPathResolver;
            _scaffoldingService = scaffoldingService;
            _model = model;
        }

        public async Task Generate()
        {
            string entityName = _model.Entity;
            string areaName = _entityReflector.GetAreaName(entityName);
            string serviceName = serviceName = $"{entityName}{ServiceSuffix}";
            var entityOperations = _entityOperationsResolver.Resolve(entityName);

            dynamic serviceTemplateModel = new ServiceTemplateModel()
            {
                EntityName = entityName,
                AreaName = areaName,
                ServiceName = serviceName,
                ServiceModelsNamespaceName = _namespaceService.GetServiceModelsNamespace(areaName),
                ServiceNamespaceName = _namespaceService.GetServiceNamespace(areaName),
                ServicesNamespaceName = _namespaceService.GetServicesNamespace(),
                FrameworkServicesNamespaceName = _namespaceService.GetFrameworkServicesNamespace(),
                EntitiesNamespaceName = _namespaceService.GetFrameworkEntitiesNamespace(),
                EntityNamespaceName = _namespaceService.GetFrameworkEntityNamespace(areaName),
                DbSetPropertyName = _entityReflector.GetDataSetPropertyName(entityName),
                EntityIdTypeName = _entityReflector.GetEntityIdType(entityName),
                CanUpdate = entityOperations.Update,
                CanDelete = entityOperations.Delete,
                CanList = entityOperations.List,
                ReadEntityDtoName = _outputClassNameResolver.GetReadDtoName(entityName),
                CreateEntityDtoName = _outputClassNameResolver.GetCreateDtoName(entityName),
                UpdateEntityDtoName = _outputClassNameResolver.GetUpdateEntityDtoName(entityName)
            };

            await _scaffoldingService.ScaffoldAsync(
                _outputPathResolver.GetServicePath(areaName),
                serviceName,
                "ServiceTemplate",
                serviceTemplateModel,
                _model.Force);
        }
    }
}
