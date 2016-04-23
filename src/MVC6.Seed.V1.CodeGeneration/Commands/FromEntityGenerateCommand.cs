using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Commands.Models;
using MVC6.Seed.V1.CodeGeneration.Services;

namespace MVC6.Seed.V1.CodeGeneration.Commands
{
    public abstract class FromEntityGenerateCommand : GenerateCommand
    {
        public FromEntityGenerateCommand(
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddServiceWithDependency<IEntityReflector, EntityReflector>();
            AddServiceWithDependency<IEntityPropertyResolver, EntityPropertyResolver>();
            AddServiceWithDependency<IEntityOperationsResolver, EntityOperationsResolver>();
        }

        public void ValidateCommandLineModel(FromEntityCommandLineModel model)
        {
            if (string.IsNullOrEmpty(model.Entity))
            {
                throw new Exception("-e is required");
            }
        }
    }
}
