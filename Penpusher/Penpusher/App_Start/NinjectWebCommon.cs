using System;
using System.Web;
using System.Web.Http;

using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using Penpusher;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Penpusher.DAL;
using Penpusher.Services;
using static Penpusher.Controllers.HomeController;
using static Penpusher.Controllers.TestController;
using WebApiContrib.IoC.Ninject;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Penpusher
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                GlobalConfiguration.Configuration.DependencyResolver = kernel.Get<System.Web.Http.Dependencies.IDependencyResolver>();
                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Bind<IDbProvider>().To<SqlServerDbProvider>();
            kernel.Bind<IArticleService>().To<ArticleService>();
            kernel.Bind<IRepository<Article>>().To<Repository<Article>>();
            kernel.Bind<INewsProviderService>().To<NewsProviderService>();
            kernel.Bind<IRepository<NewsProvider>>().To<Repository<NewsProvider>>();
        }        
    }
}
