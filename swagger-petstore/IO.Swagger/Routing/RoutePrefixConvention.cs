using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;

namespace IO.Swagger.Routing
{
    /// <summary>
    /// 
    /// </summary>
    public class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _routePrefix;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="route"></param>
        public RoutePrefixConvention(IRouteTemplateProvider route)
        {
            _routePrefix = new AttributeRouteModel(route);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        public void Apply(ApplicationModel application)
        {
            foreach (var selector in application.Controllers.SelectMany(c => c.Selectors))
            {
                if (selector.AttributeRouteModel != null)
                {
                    var routeModel = new AttributeRouteModel
                    {
                        Template = _routePrefix.Template + selector.AttributeRouteModel.Template
                    };
                    selector.AttributeRouteModel = routeModel;
                }
                else
                {
                    selector.AttributeRouteModel = _routePrefix;
                }
            }
        }
    }
}
