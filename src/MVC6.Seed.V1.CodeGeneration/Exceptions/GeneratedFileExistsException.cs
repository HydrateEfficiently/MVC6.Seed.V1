using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Exceptions
{
    public class GeneratedFileExistsException : Exception
    {
        public GeneratedFileExistsException(string outputFileName)
            : base($"The file {outputFileName} exists, use -f option to overwrite")
        {
        }
    }
}
