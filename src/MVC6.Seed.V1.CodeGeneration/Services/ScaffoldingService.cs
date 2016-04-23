using Microsoft.Extensions.CodeGeneration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.CodeGeneration.Exceptions;
using MVC6.Seed.V1.CodeGeneration.Services;
using MVC6.Seed.V1.CodeGeneration.Utility;

namespace MVC6.Seed.V1.CodeGeneration
{
    public class ScaffoldingService
    {
        private const string CodeFileExtension = "cs";
        private const string TemplateFileExtension = "cshtml";

        private readonly ICodeGeneratorActionsService _codeGeneratorActionsService;
        private readonly IApplicationEnvironment _applicationEnvironment;
        private readonly ILibraryManager _libraryManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        private readonly NamespaceService _nameResolver;
        private readonly DirectoryResolver _directoryResolver;

        public ScaffoldingService(
            ICodeGeneratorActionsService codeGeneratorActionsService,
            IApplicationEnvironment applicationEnvironment,
            ILibraryManager libraryManager,
            IServiceProvider serviceProvider,
            ILogger logger)
        {
            _codeGeneratorActionsService = codeGeneratorActionsService;
            _applicationEnvironment = applicationEnvironment;
            _libraryManager = libraryManager;
            _serviceProvider = serviceProvider;
            _logger = logger;

            _nameResolver = ActivatorUtilities.CreateInstance<NamespaceService>(_serviceProvider);
            _directoryResolver = ActivatorUtilities.CreateInstance<DirectoryResolver>(_serviceProvider);
        }

        public async Task ScaffoldAsync(
            string outputDirectory,
            string outputFileName,
            string templateName,
            object templateModel,
            bool force = false)
        {
            if (!Directory.Exists(outputDirectory))
            {
                _logger.LogMessage($"Directory {outputDirectory} did not exist, creating...");
                Directory.CreateDirectory(outputDirectory);
            }

            string outputAbsoluteFileName = Path.Combine(outputDirectory, $"{outputFileName}.{CodeFileExtension}");
            ValidateOutputFileName(outputAbsoluteFileName, force);

            string templateFolder = _directoryResolver.GetTemplateFolder();

            await _codeGeneratorActionsService.AddFileFromTemplateAsync(
                outputAbsoluteFileName, $"{templateName}.cshtml", new List<string>() { templateFolder }, templateModel);
        }

        #region Helpers

        private void ValidateOutputFileName(string outputFileName, bool force = false)
        {
            if (File.Exists(outputFileName) && !force)
            {
                throw new GeneratedFileExistsException(outputFileName);
            }
        }

        #endregion
    }
}
