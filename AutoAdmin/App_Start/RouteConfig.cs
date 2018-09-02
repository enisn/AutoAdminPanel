using System.Web.Mvc;
using System.Web.Routing;

namespace AutoAdmin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{table}/{action}/{id}",
                
                defaults: new { controller = "Home", action = "Index",table = "Categories", id = UrlParameter.Optional }
            );
        }
    }
}
