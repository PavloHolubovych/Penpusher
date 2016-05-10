using System;
using Moq;
using Ninject;
using NUnit.Framework;
using Report.Extensibility;
using Report.Service;

namespace Report.Test
{
    [TestFixture]
    public class ReportGeneratorTest: TestBase
    {
        private const string ReportGeneratorName = "ReportGenerator: ";

        [OneTimeSetUp]
        public override void Initialize()
        {
            base.Initialize();
            MockKernel.Bind<IReportGenerator>().To<ReportGenerator>();
        }

        [Category(ReportGeneratorName)]
        [TestCase("", TestName = "Empty Name")]
        [TestCase("SomeVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryLongName", TestName = "Very long Name")]
        public void GenerateReportTest(string name)
        {
            //Mock<IReportGenerator> mock = new Mock<IReportGenerator>();
            //mock.Setup(rg => rg.GenerateReport(It.IsAny<IReportInfo>())).Returns("dgsahgsdajhsdagjhsadg");
            //IReportGenerator mockObject = mock.Object;

            DateTime mockDateTime = DateTime.Now;
            string mockName = name;
            int mockSum = 123;

            var reportInfo = new ReportInfo(mockDateTime, mockName, mockSum);
            string expected = String.Format("Name: {0}, When: {1}, Sum: 123", name, mockDateTime);
            string actual = MockKernel.Get<IReportGenerator>().GenerateReport(reportInfo);
            Assert.AreEqual(expected, actual);
        }
    }
}