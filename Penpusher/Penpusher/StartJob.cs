// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartJob.cs" company="Star team">
//   This class created for start timer job
// </copyright>
// <summary>
//   The start job.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web;
using Owin;
using Penpusher.DAL;

namespace Penpusher
{
    using System.Diagnostics.CodeAnalysis;
    using Hangfire;
    using Services;

    /// <summary>
    /// The start job.
    /// </summary>
    public static class StartJob
    {
        /// <summary>
        /// The job for syncronize articles.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static void InitHangfire(this IAppBuilder app)
        {
            var options = new DashboardOptions { AppPath = VirtualPathUtility.ToAbsolute("~") };
            app.UseHangfireDashboard("/jobsArticles", options);
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                Activator = new NinjectJobActivator(NinjectWebCommon.Kernel)
            });
            var artService = new ArticleService(new Repository<Article>());
            RecurringJob.AddOrUpdate(
                "test add new article",
                () => artService.AddArticle(),
                Cron.Daily);
        }
    }
}