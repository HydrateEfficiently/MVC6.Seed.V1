using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Commands.Models
{
    public interface HasIdentityCommandLineModel
    {
        string IdentityTypeName { get; set; }
    }
}
