using Microsoft.Extensions.CodeGeneration.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Commands.Models;
using MVC6.Seed.V1.CodeGeneration.Generators.DtoMappings;
using MVC6.Seed.V1.CodeGeneration.Generators.Entity;

namespace MVC6.Seed.V1.CodeGeneration.Commands
{
    [Alias("mappings")]
    public class DtoMappingsGenerateCommand : FromEntityGenerateCommand
    {
        public DtoMappingsGenerateCommand(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task GenerateCode(FromEntityCommandLineModel model)
        {
            ValidateCommandLineModel(model);
            await GetGenerator<DtoMappingsGenerator>(model).Generate();
        }
    }
}
