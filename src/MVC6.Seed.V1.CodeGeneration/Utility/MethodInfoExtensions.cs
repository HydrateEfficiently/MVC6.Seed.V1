using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Utility
{
    public static class MethodInfoExtensions
    {
        public static bool IsAsync(this MethodInfo methodInfo)
        {
            return methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>() != null;
        }
    }
}
