// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Sigma software">
//   Global configuration
// </copyright>
// <summary>
//   The mvc application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Penpusher
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Hangfire;

    using GlobalConfiguration = System.Web.Http.GlobalConfiguration;

    /// <summary>
    /// The mvc application.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Hangfire.GlobalConfiguration.Configuration
            .UseSqlServerStorage("Server=10.40.236.195;Database=PenpusherDatabase;User Id=sa;Password = 1qaz@WSX;");

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter
            .SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}