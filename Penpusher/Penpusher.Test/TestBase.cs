namespace Penpusher.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ninject.MockingKernel.Moq;

    /// <summary>
    /// The test base.
    /// </summary>
    public class TestBase :IDisposable
    {
        /// <summary>
        /// The mock kernel.
        /// </summary>
        private MoqMockingKernel mockKernel;

        /// <summary>
        /// Gets the mock kernel.
        /// </summary>
        protected MoqMockingKernel MockKernel
        {
            get { return this.mockKernel; }
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        public virtual void Initialize()
        {
            this.mockKernel = new MoqMockingKernel();
        }

        public void Dispose()
        {
            MockKernel.Dispose();
        }
    }
}
