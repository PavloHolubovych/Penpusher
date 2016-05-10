using Ninject.MockingKernel.Moq;

namespace Report.Test
{
    public class TestBase
    {
        private MoqMockingKernel mockKernel;

        protected MoqMockingKernel MockKernel
        {
            get { return mockKernel; }
        }

        public virtual void Initialize()
        {
            mockKernel = new MoqMockingKernel();
        }
    }
}