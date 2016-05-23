// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartJob.cs" company="Star team">
//   This class created for start timer job
// </copyright>
// <summary>
//   The start job.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Penpusher
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Hangfire;

    using Ninject;

    using Services;

    /// <summary>
    /// The start job.
    /// </summary>
    public static class StartJob
    {
        /// <summary>
        /// The job for syncronize articles.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static void JobSyncArticles()
        {
            var artService = NinjectWebCommon.GetKernel.Get<IArticleService>();
            var providerService = NinjectWebCommon.GetKernel.Get<INewsProviderService>();
            NewsProvider provider = providerService.GetAll().ToArray()[0];

            RecurringJob.AddOrUpdate(
                "test job service1",
                () => artService.AddArticle(
               new Article
               {
                   Date = DateTime.Now,
                   Description = "Ti mayesh vzletiti krihitko!",
                   IdNewsProvider = provider.Id,
                   Link = "Link to sky",
                   Title = "From job"
               }),
                Cron.Daily);
        }
    }
}