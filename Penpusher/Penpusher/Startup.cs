// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Sigma">
//   Sigma software company team dream
// </copyright>
// <summary>
//   The startup.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Web.Http;

using Microsoft.Owin;

using Ninject;

using Owin;

using Penpusher.Services;

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
        /// The article service.
        /// </summary>
        private readonly IArticleService articleService;
        private readonly INewsProviderService newsProviderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="articleService">
        /// The article service.
        /// </param>
        //public Startup(IArticleService articleService)
        //{
        //    this.articleService = articleService;
        //}

        /// <summary>
        /// The configuration.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var options = new DashboardOptions { AppPath = VirtualPathUtility.ToAbsolute("~") };
            app.UseHangfireDashboard("/jobsArticles", options);
            //var cardStatusService = NinjectWebCommon.CreateKernel().Get<IArticleService>();
            //var cardStatusServiceProvider = NinjectWebCommon.CreateKernel().Get<INewsProviderService>();
            //var provider = cardStatusServiceProvider.GetAll().ToArray()[0];
            //RecurringJob.AddOrUpdate("expirationJob", () => cardStatusService.AddArticle(new Article { Title = "From Hangfire job", NewsProvider = provider, Date = DateTime.Now, Description = "dfsafdsa", Link = "linkfdsfsd", IdNewsProvider = provider.Id }), Cron.Minutely());
            config.MapHttpAttributeRoutes();
            ConfigureAuth(app);
        }
    }

    public static class StartJob
    {

    }
}