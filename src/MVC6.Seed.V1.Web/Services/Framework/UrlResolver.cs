using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MVC6.Seed.V1.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Web.Services.Framework
{
    public class UrlResolver : IUrlResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string ResolveConfirmationEmailUrl(string userId, string code)
        {
            throw new NotImplementedException();
            //return GetUrl("ConfirmEmail", "Home", new { userId = userId, code = code });
        }

        #region Helpers

        private string GetUrl(string action, string controller, object values)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var httpRequestServices = _httpContextAccessor.HttpContext.RequestServices;
            var urlHelper = httpRequestServices.GetRequiredService<IUrlHelper>();
            return urlHelper.Action(action, controller, values, httpContext.Request.Scheme);
        }

        #endregion
    }
}
