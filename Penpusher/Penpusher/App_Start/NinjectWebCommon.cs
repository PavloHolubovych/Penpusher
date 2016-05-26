using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Penpusher;
using Penpusher.DAL;
using Penpusher.Services;
using Penpusher.Services.ContentService;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace Penpusher
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();
        private static readonly Lazy<IKernel> KernelFactoryLazy = new Lazy<IKernel>(CreateKernel);

        public static IKernel Kernel => KernelFactoryLazy.Value;

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(() => Kernel);
        }

        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                var ninjectResolver = new NinjectDependencyResolver(kernel);
                DependencyResolver.SetResolver(ninjectResolver); // MVC
                GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver; // web api
                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

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