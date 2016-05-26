using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Ninject;
using NUnit.Framework;
using Penpusher.Services.ContentService;

namespace Penpusher.Test
{
    [TestFixture]
    public class NinjectTest
    {
        public static IEnumerable<object[]> ControllerTypes
        {
            get
            {
                return typeof(Startup).Assembly.GetTypes()
                    .Where(t => typeof(ApiController).IsAssignableFrom(t) || typeof(Controller).IsAssignableFrom(t) || typeof(ProviderTrackingService).IsAssignableFrom(t))
                    .Select(t => new[] { t });
            }
        }

        [Test, TestCaseSource(nameof(ControllerTypes))]
        public void AllControllersShouldBeCreatable(Type contollerType)
        {
            IKernel kernel = NinjectWebCommon.Kernel;
            Assert.NotNull(kernel.Get(contollerType));
        }
    }
}