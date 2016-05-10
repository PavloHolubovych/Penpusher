using System;
using Ninject;
using NUnit.Framework;
using Report.Extensibility;
using Report.Service;

namespace Report.Test.Integration
{
    [TestFixture]
    public class ReportServiceIntegrationTest : TestBase
    {
        [OneTimeSetUp]
        public void Initialize()
        {
            base.Initialize();
            MockKernel.Bind<IReportService>().To<ReportService>();
            MockKernel.Bind<IReportGenerator>().To<ReportGenerator>();
            MockKernel.GetMock<IDateTimeProvider>().Setup(dtp => dtp.GetNow()).Returns(DateTime.Parse("5/10/2016 3:34:29 PM"));
        }

        [TestCase()]
        public void GetReportTest()
        {
            string expected = String.Format("Name: SomeName, When: {0}, Sum: 123", DateTime.Parse("5/10/2016 3:34:29 PM"));
            string actual = MockKernel.Get<IReportService>().GetReport();

            Assert.AreEqual(expected, actual);
        }
    }
}
