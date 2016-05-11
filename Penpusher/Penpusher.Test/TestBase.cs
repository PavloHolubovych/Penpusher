using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.MockingKernel.Moq;
using Penpusher;

namespace Penpusher.Test
{
    public class TestBase
    {
        private MoqMockingKernel mockKernel;

        protected MoqMockingKernel MockKernel
        {
            get { return mockKernel; }
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        public virtual void Initialize()
        {
            mockKernel = new MoqMockingKernel();
        }
    }
}
