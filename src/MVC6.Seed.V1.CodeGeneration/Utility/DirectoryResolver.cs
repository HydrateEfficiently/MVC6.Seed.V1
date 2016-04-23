using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Utility
{
    // TODO: Tidy this class up
    public class DirectoryResolver
    {
        private readonly IApplicationEnvironment _appEnvironment;
        private readonly ILibraryManager _libraryManager;

        public DirectoryResolver(
            IApplicationEnvironment appEnvironment,
            ILibraryManager libraryManager)
        {
            _appEnvironment = appEnvironment;
            _libraryManager = libraryManager;
        }

        private string ContainingProject
        {
            get
            {
                return _libraryManager.GetLibrary(_appEnvironment.ApplicationName).Name;
            }
        }

        private Library Dependency
        {
            get
            {
                return _libraryManager.GetLibrary(ContainingProject);
            }
        }

        private string ContainingProjectDirectory
        {
            get
            {
                var dependency = Dependency;
                return Path.GetDirectoryName(Dependency.Path);
            }
        }

        public string GetTemplateFolder()
        {
            var result = Path.Combine(ContainingProjectDirectory, "Templates");

            if (!Directory.Exists(result))
            {
                throw new FileNotFoundException($"Could not find templates directory {result}");
            }

            return result;
        }
    }
}
