// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProviderTrackingService.cs" company="Sigma software">
//   IProviderTrackingService
// </copyright>
// <summary>
//   The ProviderTrackingService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Penpusher.Services.ContentService
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Penpusher.Models;

    /// <summary>
    /// The ProviderTrackingService interface.
    /// </summary>
    public interface IProviderTrackingService
    {
        /// <summary>
        /// The get updated rss files from news providers.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        IEnumerable<RssChannelModel> GetUpdatedRssFilesFromNewsProviders();

        /// <summary>
        /// The update articles from news providers.
        /// </summary>
        void UpdateArticlesFromNewsProviders();
    }
}