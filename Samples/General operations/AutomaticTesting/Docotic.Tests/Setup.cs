using BitMiracle.Docotic.Pdf.Samples.Tests.Helpers;
using NUnit.Framework;

namespace BitMiracle.Docotic.Pdf.Samples.Tests
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            DocoticLicense.Apply();
        }
    }
}
