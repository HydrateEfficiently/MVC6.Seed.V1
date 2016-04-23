using Microsoft.Extensions.CodeGeneration.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Commands.Models;

namespace MVC6.Seed.V1.CodeGeneration.Generators.Dto
{
    public class DtoCommandLineModel : FromEntityCommandLineModel
    {
        [Option(Name = "read", ShortName = "r", Description = "A flag which determines if the read DTO should be generated")]
        public bool Read { get; set; }

        [Option(Name = "create", ShortName = "c", Description = "A flag which determines if the create DTO should be generated")]
        public bool Create { get; set; }

        [Option(Name = "update", ShortName = "u", Description = "A flag which determines if the update DTO should be generated")]
        public bool Update { get; set; }
    }
}
