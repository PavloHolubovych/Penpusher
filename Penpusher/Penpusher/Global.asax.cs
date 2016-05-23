using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Hosting;

namespace Penpusher
{
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
        public class HangfireBootstrapper : IRegisteredObject
        {
            public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();

            private readonly object _lockObject = new object();
            private bool _started;

            private BackgroundJobServer _backgroundJobServer;

            private HangfireBootstrapper()
            {
            }

            public void Start()
            {
                lock (_lockObject)
                {
                    if (_started) return;
                    _started = true;

                    HostingEnvironment.RegisterObject(this);

                    Hangfire.GlobalConfiguration.Configuration
            .UseSqlServerStorage("Server=10.40.236.195;Database=PenpusherDatabase;User Id=sa;Password = 1qaz@WSX;");
                    // Specify other options here

                    _backgroundJobServer = new BackgroundJobServer();
                }
            }

            public void Stop()
            {
                lock (_lockObject)
                {
                    if (_backgroundJobServer != null)
                    {
                        _backgroundJobServer.Dispose();
                    }

                    HostingEnvironment.UnregisterObject(this);
                }
            }

            void IRegisteredObject.Stop(bool immediate)
            {
                Stop();
            }
        }
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
            //Hangfire.GlobalConfiguration.Configuration
            //.UseSqlServerStorage("Server=10.40.236.195;Database=PenpusherDatabase;User Id=sa;Password = 1qaz@WSX;");
            HangfireBootstrapper.Instance.Start();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter
            .SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
        protected void Application_End(object sender, EventArgs e)
        {
            HangfireBootstrapper.Instance.Stop();
        }
    }
}