using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Generators.ApiController
{
    public class ServiceMethodInvocationModel
    {
        public string BaseName { get; internal set; }
        public bool ReturnsValue { get; internal set; }
        public bool IsAsync { get; internal set; }

        public string Name
        {
            get
            {
                var result = BaseName;
                if (IsAsync)
                {
                    result += "Async";
                }
                return result;
            }
        }
    }
}
