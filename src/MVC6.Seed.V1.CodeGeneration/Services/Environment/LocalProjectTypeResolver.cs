using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Services.Environment
{
    public interface ILocalProjectTypeResolver
    {
        LocalProjectType Resolve(string commandLineOption);
    }

    public class LocalProjectTypeResolver : ILocalProjectTypeResolver
    {
        public LocalProjectType Resolve(string commandLineOption)
        {
            switch (commandLineOption.ToLowerInvariant())
            {
                case "f":
                    return LocalProjectType.Framework;
                case "s":
                    return LocalProjectType.Services;
                case "w":
                    return LocalProjectType.Web;
                default:
                    throw new InvalidOperationException($"Could not resolve local project type from command line option {commandLineOption}");
            }
        }
    }
}
