using Microsoft.Extensions.CodeGeneration.CommandLine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Commands;
using MVC6.Seed.V1.CodeGeneration.Commands.Models;

namespace MVC6.Seed.V1.CodeGeneration.Generators.Entity
{
    public class EntityCommandLineModel : FromEntityCommandLineModel,
        HasIdentityCommandLineModel
    {
        [Option(Name = "area", ShortName = "a", Description = "The namespace name below the root entities namespace.")]
        public string Area { get; set; }

        [Option(Name = "identity", ShortName = "i", Description = "The type of the identity property, \"Id\", g: guid (default), i: int")]
        public string IdentityTypeName { get; set; }

        [Option(Name = "audit", ShortName = "au", Description = "The audit foreign keys.")]
        public string Audits { get; set; }

        [Option(Name = "properties", ShortName = "p", Description = "The comma seperated properties of the entity, in form <property-type>:<property-name>")]
        public string Properties { get; set; }

        [Option(Name = "table", ShortName = "t", Description = "The table name.")]
        public string Table { get; set; }
    }
}
