using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Core;
using MVC6.Seed.V1.CodeGeneration.Services;
using MVC6.Seed.V1.CodeGeneration.Services.Audits;
using MVC6.Seed.V1.CodeGeneration.Services.Properties;
using MVC6.Seed.V1.CodeGeneration.Utility;
using MVC6.Seed.V1.Framework.Models;

namespace MVC6.Seed.V1.CodeGeneration.Generators.Entity
{
    public class EntityGenerator : IGenerator
    {
        private readonly ScaffoldingService _scaffoldingService;
        private readonly OutputPathResolver _outputPathResolver;
        private readonly NamespaceService _namespaceService;
        private readonly IPropertyDeclarationFactory _propertyDeclarationFactory;
        private readonly IAuditDeclarationFactory _auditDeclarationFactory;
        private readonly ITypeNameShortcutMapper _typeNameShortcutMapper;
        private readonly IEntityReflector _entityReflector;
        private readonly IDtoGenerateIgnoreAttributeFactory _dtoGenerateIgnoreAttributeFactory;
        private readonly EntityCommandLineModel _model;

        public EntityGenerator(
            ScaffoldingService scaffoldingService,
            OutputPathResolver outputPathResolver,
            NamespaceService namespaceService,
            IPropertyDeclarationFactory propertyDeclarationFactory,
            IAuditDeclarationFactory auditDeclarationFactory,
            ITypeNameShortcutMapper typeNameShortcutMapper,
            IEntityReflector entityReflector,
            IDtoGenerateIgnoreAttributeFactory dtoGenerateIgnoreAttributeFactory,
            EntityCommandLineModel model)
        {
            _scaffoldingService = scaffoldingService;
            _outputPathResolver = outputPathResolver;
            _namespaceService = namespaceService;
            _propertyDeclarationFactory = propertyDeclarationFactory;
            _auditDeclarationFactory = auditDeclarationFactory;
            _typeNameShortcutMapper = typeNameShortcutMapper;
            _entityReflector = entityReflector;
            _dtoGenerateIgnoreAttributeFactory = dtoGenerateIgnoreAttributeFactory;
            _model = model;
        }

        public async Task Generate()
        {
            var properties = new List<PropertyDeclarationModel>();
            var interfaces = new List<string>();

            string areaName = _model.Area;
            string entityName = _model.Entity;

            string identityTypeName;
            switch (_model.IdentityTypeName)
            {
                case "g":
                    identityTypeName = "Guid";
                    break;
                case "i":
                    identityTypeName = "int";
                    break;
                default:
                    throw new InvalidOperationException("Invalid value for -i");
            }
            properties.Add(_propertyDeclarationFactory.CreateProperty(
                "Id",
                identityTypeName,
                new PropertyDeclarationModelOptions()
                {
                    Attributes = new List<string>()
                    {
                        _dtoGenerateIgnoreAttributeFactory.CreateAttributeString(DtoType.Create)
                    }
                }));

            if (!string.IsNullOrWhiteSpace(_model.Properties))
            {
                var propertyDefinitions = _model.Properties.Split(',').Select(s => s.Trim());
                foreach (var propertyDefinition in propertyDefinitions)
                {
                    string[] propertyTypeNamePair = propertyDefinition.Split(':');

                    string propertyTypeName = propertyTypeNamePair[0];
                    Type foreignKeyEntity = null;
                    if (_entityReflector.TryGetEntityType(propertyTypeName, out foreignKeyEntity))
                    {
                        string propertyName = propertyTypeNamePair.Length == 2 ?
                            propertyTypeNamePair[1] : propertyTypeName;

                        properties.Add(_propertyDeclarationFactory.CreateProperty(
                            $"{propertyName}Id",
                            _entityReflector.GetEntityIdType(propertyTypeName)));
                        properties.Add(_propertyDeclarationFactory.CreateProperty(
                            propertyName,
                            propertyTypeName,
                            new PropertyDeclarationModelOptions()
                            {
                                IsVirtual = true
                            }));
                    }
                    else
                    {
                        bool isNullable = propertyTypeName.EndsWith("?");
                        string underlyingTypeName = isNullable ? propertyTypeName.Replace("?", string.Empty) : propertyTypeName;
                        underlyingTypeName = _typeNameShortcutMapper.Map(underlyingTypeName);
                        if (isNullable)
                        {
                            underlyingTypeName += "?";
                        }
                        string propertyName = propertyTypeNamePair[1];
                        properties.Add(_propertyDeclarationFactory.CreateProperty(propertyName, underlyingTypeName));
                    }                    
                }
            }

            var audits = new List<AuditDeclarationModel>();
            if (!string.IsNullOrWhiteSpace(_model.Audits))
            {
                foreach (var auditCommand in _model.Audits.Split(','))
                {
                    var auditDeclarationModel = _auditDeclarationFactory.CreateAuditProperties(auditCommand);
                    audits.Add(auditDeclarationModel);
                    if (auditDeclarationModel.IsFromInterface)
                    {
                        interfaces.Add(auditDeclarationModel.FromInterface);
                    }
                }
            }

            var templateModel = new EntityTemplateModel()
            {
                EntityName = entityName,
                AreaName = areaName,
                FrameworkEntityNamespace = _namespaceService.GetFrameworkEntityNamespace(areaName),
                FrameworkEntitiesNamespace = _namespaceService.GetFrameworkEntitiesNamespace(),
                Interfaces = interfaces,
                Properties = properties,
                Audits = audits,
                TableName = string.IsNullOrEmpty(_model.Table) ? $"{entityName}s" : _model.Table,
                GenerationCoreNamespace = _namespaceService.GetGenerationCoreNamespace()
            };

            await _scaffoldingService.ScaffoldAsync(
                _outputPathResolver.GetFrameworkEntityNamespace(areaName),
                entityName,
                "EntityTemplate",
                templateModel,
                _model.Force);
        }
    }
}
