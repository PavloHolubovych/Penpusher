using System;
using System.Web;
using System.Web.Http;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Penpusher;
using Penpusher.DAL;
using Penpusher.Services;
using Penpusher.Services.ContentService;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Penpusher
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(() => Kernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        private static Lazy<IKernel> kernelFactoryLazy = new Lazy<IKernel>(CreateKernel);

        public static IKernel Kernel => kernelFactoryLazy.Value;

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

                GlobalConfiguration.Configuration.DependencyResolver =
                    kernel.Get<System.Web.Http.Dependencies.IDependencyResolver>();
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
            kernel.Bind<IArticleService>().To<ArticleService>();
            kernel.Bind<IRepository<Article>>().To<Repository<Article>>();
            kernel.Bind<INewsProviderService>().To<NewsProviderService>();
            kernel.Bind<IRepository<NewsProvider>>().To<Repository<NewsProvider>>();
            kernel.Bind<IRepository<UsersNewsProvider>>().To<Repository<UsersNewsProvider>>();
            kernel.Bind<IUsersArticlesService>().To<UsersArticlesService>();
            kernel.Bind<IRepository<UsersArticle>>().To<Repository<UsersArticle>>();
            kernel.Bind<IParser>().To<RssParser>();
            kernel.Bind<IProviderTrackingService>().To<ProviderTrackingService>();
            kernel.Bind<IDataBaseServiceExtension>().To<DataBaseServiceExtension>();
            kernel.Bind<IRssReader>().To<RssReader>();
        }
    }
}