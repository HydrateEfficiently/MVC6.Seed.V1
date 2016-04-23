using Microsoft.Extensions.CodeGeneration.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Generators.Dto;

namespace MVC6.Seed.V1.CodeGeneration.Commands
{
    [Alias("dto")]
    public class DtoGenerateCommand : FromEntityGenerateCommand
    {
        public DtoGenerateCommand(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task GenerateCode(DtoCommandLineModel model)
        {
            ValidateCommandLineModel(model);
            await GetGenerator<DtoGenerator>(model).Generate();
        }
    }
}
