using System.Web.Http;

namespace Penpusher
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApiWithActionAndId", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
        }
    }
}