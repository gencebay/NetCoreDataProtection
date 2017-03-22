using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

namespace NetCore.Hosting
{
    public static class UrlGeneratorExtensions
    {
        private static DefaultDataProtectorProvider GetProtector(ActionContext context)
        {
            return ServiceProviderServiceExtensions.GetService<DefaultDataProtectorProvider>(context.HttpContext.RequestServices);
        }

        public static string ToQueryString(this NameValueCollection values)
        {
            var array = (from key in values.AllKeys
                         from value in values.GetValues(key)
                         select string.Format("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(value))).ToArray();

            return "?" + string.Join("&", array);
        }

        public static string CreateProtectedUrl(this IUrlHelper urlHelper, 
            ActionContext context, 
            string actionName, 
            string controllerName, 
            object routeValues)
        {
            #region Null Checks
            #endregion

            var values = new NameValueCollection();
            var dictionary = new RouteValueDictionary(routeValues);
            foreach (KeyValuePair<string, object> entry in dictionary)
            {
                if (entry.Value != null)
                    values.Add(entry.Key, entry.Value.ToString());
            }

            var protectedQueryString = WebUtility.UrlEncode(GetProtector(context).Protect(values.ToQueryString()));
            var url = $"/{controllerName}/{actionName}?{Globals.QueryStringPrefix}={protectedQueryString}";
            return url;
        }
    }
}