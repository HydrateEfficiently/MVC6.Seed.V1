using Microsoft.Extensions.CodeGeneration.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Generators.ApiController;

namespace MVC6.Seed.V1.CodeGeneration.Commands
{
    [Alias("api")]
    public class ApiControllerGenerateComand : GenerateCommand
    {
        public ApiControllerGenerateComand(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task GenerateCode(ApiControllerCommandLineModel model)
        {
            await GetGenerator<ApiControllerGenerator>(model).Generate();
        }
    }
}
