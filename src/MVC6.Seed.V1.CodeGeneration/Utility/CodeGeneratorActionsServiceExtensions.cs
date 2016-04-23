using Microsoft.Extensions.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Utility
{
    public static class CodeGeneratorActionsServiceExtensions
    {
        public static async Task AddFileFromTemplateAsync(
            this ICodeGeneratorActionsService codeGeneratorActionsService,
            string outputPath,
            string templateName,
            string templateFolder,
            object templateModel)
        {
            await codeGeneratorActionsService.AddFileFromTemplateAsync(
                outputPath, templateName, new List<string>() { templateFolder }, templateModel);
        }
    }
}
