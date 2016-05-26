using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Penpusher.Startup))]

namespace Penpusher
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            ConfigureAuth(app);

            // Initialization of timer job. Don't use in debug
#if !DEBUG
            app.InitHangfire();
#endif
        }
    }
}