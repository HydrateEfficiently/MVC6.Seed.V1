using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Services.Properties
{
    public interface IPropertyDeclarationFactory
    {
        PropertyDeclarationModel CreateProperty(
            string propertyName,
            string resolvedPropertyTypeName,
            PropertyDeclarationModelOptions options = null);

        IEnumerable<PropertyDeclarationModel> CreateInterfaceProperties(
            Type interfaceType,
            Dictionary<string, PropertyDeclarationModelOptions> optionsByPropertyName = null);
    }

    public class PropertyDeclarationFactory : IPropertyDeclarationFactory
    {
        private readonly IPropertyTypeNameResolver _propertyTypeNameResolver;

        public PropertyDeclarationFactory(
            IPropertyTypeNameResolver propertyTypeNameResolver)
        {
            _propertyTypeNameResolver = propertyTypeNameResolver;
        }

        public PropertyDeclarationModel CreateProperty(
            string propertyName,
            string resolvedPropertyTypeName,
            PropertyDeclarationModelOptions options = null)
        {
            if (options == null)
            {
                options = new PropertyDeclarationModelOptions();
            }

            string extraModifiers = string.Join(" ", new Dictionary<string, bool>()
            {
                { "virtual", options.IsVirtual }
            }.Where(kvp => kvp.Value).Select(kvp => kvp.Key));

            return new PropertyDeclarationModel()
            {
                AccessModifier = options.AccessModifier,
                ExtraModifiers = extraModifiers,
                PropertyTypeName = resolvedPropertyTypeName,
                PropertyName = propertyName,
                Attributes = options.Attributes
            };
        }

        public IEnumerable<PropertyDeclarationModel> CreateInterfaceProperties(
            Type interfaceType,
            Dictionary<string, PropertyDeclarationModelOptions> optionsByPropertyName = null)
        {
            if (optionsByPropertyName == null)
            {
                optionsByPropertyName = new Dictionary<string, PropertyDeclarationModelOptions>();
            }

            var result = new List<PropertyDeclarationModel>();
            var properties = interfaceType.GetProperties();
            foreach (var pi in properties)
            {
                string propertyName = pi.Name;
                PropertyDeclarationModelOptions options;
                if (!optionsByPropertyName.TryGetValue(propertyName, out options))
                {
                    options = new PropertyDeclarationModelOptions();
                }

                result.Add(CreateProperty(
                    propertyName,
                    _propertyTypeNameResolver.Resolve(pi),
                    options
                ));
            }
            return result;
        }
    }
}
