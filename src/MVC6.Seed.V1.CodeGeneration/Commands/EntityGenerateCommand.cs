using Microsoft.Extensions.CodeGeneration.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Commands;
using MVC6.Seed.V1.CodeGeneration.Generators.DataContext;
using MVC6.Seed.V1.CodeGeneration.Generators.Entity;

namespace MVC6.Seed.V1.CodeGeneration.Entity
{
    [Alias("entity")]
    public class EntityGenerateCommand : FromEntityGenerateCommand
    {
        public EntityGenerateCommand(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task GenerateCode(EntityCommandLineModel model)
        {
            ValidateCommandLineModel(model);

            if (string.IsNullOrWhiteSpace(model.Area))
            {
                throw new InvalidOperationException("Please specify an area with -a");
            }
            
            if (string.IsNullOrWhiteSpace(model.IdentityTypeName))
            {
                model.IdentityTypeName = "g";
            }
            else if (model.IdentityTypeName != "g" && model.IdentityTypeName != "i")
            {
                throw new InvalidOperationException("Invalid value for -i");
            }

            var entityGenerator = GetGenerator<EntityGenerator>(model);
            await entityGenerator.Generate();

            var dbContextGenerator = GetGenerator<DataContextGenerator>(new DataContextCommandLineModel()
            {
                Force = model.Force
            });
            await dbContextGenerator.Generate();
        }
    }
}
