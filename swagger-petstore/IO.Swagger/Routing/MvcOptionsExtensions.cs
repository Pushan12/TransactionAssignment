using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace IO.Swagger.Routing
{
    /// <summary>
    /// 
    /// </summary>
    public static class MvcOptionsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opts"></param>
        /// <param name="routeAttribute"></param>
        public static void UseGeneralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Add(new RoutePrefixConvention(routeAttribute));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opts"></param>
        /// <param name="prefix"></param>
        public static void UseGeneralRoutePrefix(this MvcOptions opts, string prefix)
        {
            opts.UseGeneralRoutePrefix(new RouteAttribute(prefix));
        }
    }
}
