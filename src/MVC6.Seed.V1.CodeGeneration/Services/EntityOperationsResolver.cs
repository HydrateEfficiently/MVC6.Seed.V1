using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.Framework.Models;

namespace MVC6.Seed.V1.CodeGeneration.Services
{
    public interface IEntityOperationsResolver
    {
        EntityOperations Resolve(string entityName);
    }

    public class EntityOperationsResolver : IEntityOperationsResolver
    {
        private readonly IEntityReflector _entityReflector;

        private Dictionary<string, EntityOperations> _entityOperationsByName =
            new Dictionary<string, EntityOperations>();

        public EntityOperationsResolver(
            IEntityReflector entityReflector)
        {
            _entityReflector = entityReflector;
        }

        // TODO: Resolve from command line model so we can override default values
        public EntityOperations Resolve(string entityName)
        {
            EntityOperations result;
            if (!_entityOperationsByName.TryGetValue(entityName, out result))
            {
                var entityType = _entityReflector.GetEntityType(entityName);
                result = new EntityOperations(
                    create: true,
                    read: true,
                    update: typeof(IUpdateAuditable).IsAssignableFrom(entityType),
                    delete: typeof(IDeleteAuditable).IsAssignableFrom(entityType),
                    list: true);
                _entityOperationsByName.Add(entityName, result);
            }
            return result;
        }
    }

    public class EntityOperations
    {
        public bool Create { get; private set; }

        public bool Read { get; private set; }

        public bool Update { get; private set; }

        public bool Delete { get; private set; }

        public bool List { get; private set; }

        public EntityOperations(bool create, bool read, bool update, bool delete, bool list)
        {
            Create = create;
            Read = read;
            Update = update;
            List = list;
            Delete = delete;
        }
    }
}
