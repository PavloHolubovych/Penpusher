// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Sigma">
//   Sigma software company team dream
// </copyright>
// <summary>
//   The startup.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;

using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(Penpusher.Startup))]

namespace Penpusher
{
    using System.Web;

    using Hangfire;

    /// <summary>
    /// The startup.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            ConfigureAuth(app);

            // app.InitHangfire();
        }
    }
}