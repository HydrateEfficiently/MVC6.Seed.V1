using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Core;
using MVC6.Seed.V1.CodeGeneration.Exceptions;
using MVC6.Seed.V1.CodeGeneration.Services;
using MVC6.Seed.V1.CodeGeneration.Services.Properties;
using MVC6.Seed.V1.CodeGeneration.Utility;

namespace MVC6.Seed.V1.CodeGeneration.Generators.Dto
{
    public class DtoGenerator : IGenerator
    {
        private readonly IEntityReflector _entityReflector;
        private readonly IEntityAliasResolver _entityAliasResolver;
        private readonly IEntityOperationsResolver _entityOperationsResolver;
        private readonly IDtoAliasResolver _dtoAliasResolver;
        private readonly IPropertyTypeNameResolver _propertyNameResolver;
        private readonly NamespaceService _namespaceService;
        private readonly OutputPathResolver _outputPathResolver;
        private readonly ScaffoldingService _scaffoldingService;
        private readonly DtoCommandLineModel _model;

        public DtoGenerator(
            IEntityReflector entityReflector,
            IEntityAliasResolver entityAliasResolver,
            IEntityOperationsResolver entityOperationsResolver,
            IDtoAliasResolver dtoAliasResolver,
            IPropertyTypeNameResolver propertyNameResolver,
            NamespaceService namespaceService,
            OutputPathResolver outputPathResolver,
            ScaffoldingService scaffoldingService,
            DtoCommandLineModel model)
        {
            _entityReflector = entityReflector;
            _entityAliasResolver = entityAliasResolver;
            _entityOperationsResolver = entityOperationsResolver;
            _dtoAliasResolver = dtoAliasResolver;
            _propertyNameResolver = propertyNameResolver;
            _namespaceService = namespaceService;
            _outputPathResolver = outputPathResolver;
            _scaffoldingService = scaffoldingService;
            _model = model;
        }

        public async Task Generate()
        {
            bool generateReadDto = false;
            bool generateCreateDto = false;
            bool generateUpdateDto = false;

            if (!_model.Read && !_model.Create && !_model.Update)
            {
                var entityOperations = _entityOperationsResolver.Resolve(_model.Entity);
                generateReadDto = entityOperations.Read;
                generateCreateDto = entityOperations.Create;
                generateUpdateDto = entityOperations.Update;
            }
            else
            {
                generateReadDto = _model.Read;
                generateCreateDto = _model.Create;
                generateUpdateDto = _model.Update;
            }

            var generateTasks = new List<Task>();
            if (generateReadDto)
            {
                generateTasks.Add(Generate(DtoType.Read, _model.Force));
            }
            if (generateCreateDto)
            {
                generateTasks.Add(Generate(DtoType.Create, _model.Force));
            }
            if (generateUpdateDto)
            {
                generateTasks.Add(Generate(DtoType.Update, _model.Force));
            }
            await Task.WhenAll(generateTasks);
        }

        private async Task Generate(DtoType dtoType, bool force)
        {
            string entityName = _model.Entity;
            string areaName = _entityReflector.GetAreaName(entityName);
            string dtoName = _dtoAliasResolver.Resolve(entityName, dtoType);
            string namespaceName = _namespaceService.GetServiceModelsNamespace(areaName);

            var properties = new List<PropertyDeclarationModel>();
            var dtoProperties = new List<DtoPropertyDeclarationModel>();
            var dtoNamespaceNames = new List<string>();

            var entityType = _entityReflector.GetEntityType(entityName);
            var propertiesToGenerate = entityType.GetProperties()
                .Where(pi => !pi.ShouldIgnoreForDto(dtoType));

            foreach (var property in propertiesToGenerate)
            {
                string propertyTypeName = property.PropertyType.Name;
                Type foreignKeyEntityType;
                if (_entityReflector.TryGetEntityType(propertyTypeName, out foreignKeyEntityType))
                {
                    string basePropertyTypeName = _entityAliasResolver.Resolve(propertyTypeName);
                    var propertyDeclarationModel = new DtoPropertyDeclarationModel()
                    {
                        BasePropertyTypeName = basePropertyTypeName,
                        PropertyTypeName = _dtoAliasResolver.Resolve(basePropertyTypeName, dtoType),
                        PropertyName = property.Name
                    };
                    dtoProperties.Add(propertyDeclarationModel);
                    properties.Add(propertyDeclarationModel);

                    string foreignKeyAreaName = _entityReflector.GetAreaName(propertyTypeName);
                    string dtoNamespaceName = _namespaceService.GetServiceNamespace(foreignKeyAreaName);
                    if (dtoNamespaceName != namespaceName &&
                        !dtoNamespaceNames.Contains(dtoNamespaceName))
                    {
                        dtoNamespaceNames.Add(dtoNamespaceName);
                    }
                }
                else
                {
                    properties.Add(new PropertyDeclarationModel()
                    {
                        PropertyName = property.Name,
                        PropertyTypeName = _propertyNameResolver.Resolve(property)
                    });
                }
            }

            dynamic templateModel = new DtoTemplateModel()
            {
                EntityName = entityName,
                EntityNamespaceName = _namespaceService.GetFrameworkEntityNamespace(areaName),
                DtoName = dtoName,
                NamespaceName = namespaceName,
                DtoNamespaceNames = dtoNamespaceNames,
                DtoProperties = dtoProperties,
                Properties = properties
            };

            string dtoTemplate;
            string dtoGeneratedTemplate;
            switch (dtoType)
            {
                case DtoType.Read:
                    dtoTemplate = "ReadDtoTemplate";
                    dtoGeneratedTemplate = "ReadDtoGeneratedTemplate";
                    break;
                case DtoType.Create:
                    dtoTemplate = "CreateDtoTemplate";
                    dtoGeneratedTemplate = "CreateDtoGeneratedTemplate";
                    break;
                case DtoType.Update:
                    dtoTemplate = "UpdateDtoTemplate";
                    dtoGeneratedTemplate = "UpdateDtoGeneratedTemplate";
                    break;
                default:
                    throw new InvalidOperationException();
            }

            try
            {
                await _scaffoldingService.ScaffoldAsync(
                    _outputPathResolver.GetServiceModelsPath(areaName),
                    dtoName,
                    dtoTemplate,
                    templateModel,
                    _model.Force);
            }
            catch (GeneratedFileExistsException)
            {
            }

            await _scaffoldingService.ScaffoldAsync(
                _outputPathResolver.GetServiceModelsPath(areaName),
                $"{dtoName}.Generated",
                dtoGeneratedTemplate,
                templateModel,
                force: true);
        }
    }
}
