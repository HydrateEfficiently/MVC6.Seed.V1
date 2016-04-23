using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using MVC6.Seed.V1.Framework.Utility;
using MVC6.Seed.V1.Web.Utility;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Web.Services
{
    public class ActionEnumerateIgnoreAttribute : Attribute { }

    public class ActionEnumerator
    {
        private const string ControllerSuffix = "Controller";
        private const string ApiSuffix = "Api";

        private static Dictionary<string, Dictionary<string, ExpandoObject>> __actionsByController =
            new Dictionary<string, Dictionary<string, ExpandoObject>>();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ActionEnumerator(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ExpandoObject Enumerate(params Type[] controllers)
        {
            dynamic result = new ExpandoObject();
            foreach (var controller in controllers)
            {
                if (!controller.Name.EndsWith(ControllerSuffix))
                {
                    throw new InvalidOperationException($"Expected controller name to end with \"${ControllerSuffix}\"");
                }

                string controllerName = controller.Name.Substring(0, controller.Name.Length - ControllerSuffix.Length);
                bool isApiController = controllerName.EndsWith(ApiSuffix);
                string baseControllerName = isApiController ?
                    controllerName.Substring(0, controllerName.Length - ApiSuffix.Length) :
                    controllerName;

                Dictionary<string, ExpandoObject> actionsByActionName;
                string controllerKey = isApiController ? $"{ApiSuffix}.{baseControllerName}" : baseControllerName;
                if (!__actionsByController.TryGetValue(controllerKey, out actionsByActionName))
                {
                    actionsByActionName = GetActionsByName(controller, controllerName);
                    __actionsByController.Add(controllerKey, actionsByActionName);
                }

                dynamic container = result;
                if (isApiController)
                {
                    if (!ExpandoObjectUtility.TryGet(result, ApiSuffix, out container))
                    {
                        container = new ExpandoObject();
                        ExpandoObjectUtility.Add(result, ApiSuffix, container);
                    }
                }
                ExpandoObjectUtility.Add(container, baseControllerName, actionsByActionName);
            }
            return result;
        }

        private Dictionary<string, ExpandoObject> GetActionsByName(
            Type controller,
            string controllerName)
        {
            var result = new Dictionary<string, ExpandoObject>();

            var publicActions = ((TypeInfo)controller).DeclaredMethods.Where(mi =>
                mi.IsPublic && !mi.IsDefined(typeof(ActionEnumerateIgnoreAttribute), false));

            foreach (var actionType in publicActions)
            {
                var parameterNames = actionType
                    .GetParameters()
                    .Where(pi => !pi.IsDefined(typeof(FromBodyAttribute), false))
                    .Select(pi => pi.Name);
                var routeValues = new RouteValueDictionary();
                parameterNames.ForEach(p => routeValues.Add(p, $":{p}"));

                string actionName = actionType.Name;
                dynamic action = new ExpandoObject();

                action.UrlPattern = _httpContextAccessor.GetUrlHelper().Action(actionName, controllerName, routeValues);
                action.Method = actionType.IsDefined(typeof(HttpPostAttribute)) ? "post" : "get";
                action.ParameterNames = parameterNames;

                result.Add(actionName, action);
            }

            return result;
        }
    }
}
