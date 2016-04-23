using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.CodeGeneration.Utility
{
    public static class StringExtensions
    {
        public static string ToFirstCharacterLower(this string str)
        {
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}
