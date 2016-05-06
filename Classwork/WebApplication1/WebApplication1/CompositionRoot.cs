using System;
using Ninject;
using Ninject.Extensions.Conventions;

namespace WebApplication1
{
    public static class CompositionRoot
    {
        private static readonly Lazy<IKernel> kernelFactory = new Lazy<IKernel>(Createkernel);

        public static IKernel Kernel
        {
            get { return kernelFactory.Value; }
        }

        private static IKernel Createkernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind(m => m.FromThisAssembly()
                .SelectAllClasses()
                .BindDefaultInterface());
            return kernel;
        }
    }
}