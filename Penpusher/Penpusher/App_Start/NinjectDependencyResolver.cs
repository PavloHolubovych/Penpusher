// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NinjectDependencyResolver.cs" company="Sigma software">
//   NinjectDependencyResolver
// </copyright>
// <summary>
//   Defines the NinjectDependencyResolver type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Penpusher
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Http.Dependencies;

    using Ninject;

    /// <summary>
    /// The ninject dependency resolver.Source: http://blog.developers.ba/simple-way-share-container-mvc-web-api/
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyResolver"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// The begin scope.
        /// </summary>
        /// <returns>
        /// The <see cref="IDependencyScope"/>.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(this.kernel.BeginBlock());
        }
    }
}