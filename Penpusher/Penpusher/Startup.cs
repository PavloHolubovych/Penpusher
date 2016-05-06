using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(Penpusher.Startup))]
namespace Penpusher
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            // app.UseNinjectMiddleware(() => CompositionRoot.Kernel)
            //      .UseNinjectWebApi(config);


            config.MapHttpAttributeRoutes();
            //app.UseWebApi(config);
            ConfigureAuth(app);
        }
    }
}
