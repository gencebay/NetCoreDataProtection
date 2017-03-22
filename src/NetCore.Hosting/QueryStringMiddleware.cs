using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCore.Hosting
{
    public class QueryStringMiddleware
    {
        private readonly RequestDelegate next;

        public QueryStringMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, DefaultDataProtectorProvider protector)
        {
            if (httpContext.Request.Method == HttpMethod.Get.Method &&
                httpContext.Request.Query.ContainsKey(Globals.QueryStringPrefix))
            {
                var qs = httpContext.Request.Query[Globals.QueryStringPrefix].ToString();
                var unprotected = WebUtility.UrlDecode(protector.Unprotect(qs));
                httpContext.Request.QueryString = QueryString.Create(QueryHelpers.ParseQuery(unprotected));
            }

            // pass downstream.  
            await next(httpContext);
        }
    }
}