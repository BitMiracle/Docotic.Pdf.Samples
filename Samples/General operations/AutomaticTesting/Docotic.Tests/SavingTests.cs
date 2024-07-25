using BitMiracle.Docotic.Pdf.Samples.Tests.Helpers;
using NUnit.Framework;

namespace BitMiracle.Docotic.Pdf.Samples.Tests
{
    public class SavingTests
    {
        [TestCaseSource(nameof(OpenSaveTestCase))]
        public void OpenSave(string fileName, string password)
        {
            string inputPath = fileName.ToInput();
            string outputName = $"OpenSave_{fileName}";

            PdfSaver.Save(inputPath, password, outputName.ToOutput());

            CompareDocuments(outputName);
        }

        internal static void CompareDocuments(string outputName)
        {
            string outputPath = outputName.ToOutput();
            string expectedPath = outputName.ToExpected();

            bool equal = PdfDocument.DocumentsAreEqual(outputPath, expectedPath);
            Assert.That(equal);
            File.Delete(outputPath);
        }

        public static readonly object[] OpenSaveTestCase =
        {
            new object[] { "BRAILLE CODES WITH TRANSLATION.pdf", "" },
            new object[] { "ComboBoxes.pdf", "" },
            new object[] { "encrypted.pdf", "user" },
        };
    }
}
