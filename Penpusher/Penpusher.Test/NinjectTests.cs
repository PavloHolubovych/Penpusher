// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NinjectTests.cs" company="Banda">
//   Test for kernel
// </copyright>
// <summary>
//   The article service test class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Penpusher.Services.ContentService;

namespace Penpusher.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;

    using Ninject;


    using NUnit.Framework;

    /// <summary>
    /// The article service test class.
    /// </summary>
    [TestFixture]
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2200:CheckTestClassName", Justification = "Reviewed. Suppression is OK here.")]
    public class NinjectTests
    {
        /// <summary>
        /// Gets the controller types.
        /// </summary>
        public static IEnumerable<object[]> ControllerTypes
        {
            get
            {
                return typeof(Startup).Assembly.GetTypes()
                    .Where(t => typeof(ApiController).IsAssignableFrom(t) || typeof(Controller).IsAssignableFrom(t) || typeof(ProviderTrackingService).IsAssignableFrom(t))
                    .Select(t => new[] { t });
            }
        }

        /// <summary>
        /// The all controllers should be creatable.
        /// </summary>
        /// <param name="contollerType">
        /// The contoller type.
        /// </param>
        [Test, TestCaseSource(nameof(ControllerTypes))]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void AllControllersShouldBeCreatable(Type contollerType)
        {
            IKernel kernel = NinjectWebCommon.Kernel;
            Assert.NotNull(kernel.Get(contollerType));
        }
    }
}