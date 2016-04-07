using Microsoft.AspNet.Mvc.ModelBinding;
using MVC6.Seed.V1.Services.Identity;
using MVC6.Seed.V1.Services.Identity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Web.Utility
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddIdentityErrors(
            this ModelStateDictionary modelState,
            IdentityErrorException ex)
        {
            foreach (string error in ex.Errors)
            {
                modelState.AddModelError(string.Empty, error);
            }
        }
    }
}
