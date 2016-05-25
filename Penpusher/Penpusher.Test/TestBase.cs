using System;
using Ninject.MockingKernel.Moq;

namespace Penpusher.Test
{
    public class TestBase : IDisposable
    {
        private MoqMockingKernel mockKernel;

        protected MoqMockingKernel MockKernel => mockKernel;

        public virtual void Testinitialize()
        {
            mockKernel = new MoqMockingKernel();
        }

        public void Dispose()
        {
            MockKernel.Dispose();
        }
    }
}