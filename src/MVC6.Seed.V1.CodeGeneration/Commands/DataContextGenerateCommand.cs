using Microsoft.Extensions.CodeGeneration.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Generators.DataContext;

namespace MVC6.Seed.V1.CodeGeneration.Commands
{
    [Alias("data-context")]
    public class DataContextGenerateCommand : GenerateCommand
    {
        public DataContextGenerateCommand(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task GenerateCode(DataContextCommandLineModel model)
        {
            await GetGenerator<DataContextGenerator>(model).Generate();
        }
    }
}
