using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Initializer.Services.DataImportServices
{
    public class JsonDataImportService : IDataImportService
    {
        private readonly IApplicationEnvironment _applicationEnvironment;
        private readonly DataImportOptions _options;

        public JsonDataImportService(
            IApplicationEnvironment applicationEnvironment,
            IOptions<DataImportOptions> options)
        {
            _applicationEnvironment = applicationEnvironment;
            _options = options.Value;
        }

        public T Deserialize<T>(string fileName)
        {
            var path = Path.Combine(_applicationEnvironment.ApplicationBasePath, _options.DirectoryName, $"{fileName}.json");
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
