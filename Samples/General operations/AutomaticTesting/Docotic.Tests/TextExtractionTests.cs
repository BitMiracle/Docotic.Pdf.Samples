using BitMiracle.Docotic.Pdf.Samples.Tests.Helpers;
using NUnit.Framework;

namespace BitMiracle.Docotic.Pdf.Samples.Tests
{
    public class TextExtractionTests
    {
        [TestCaseSource(nameof(ExtractTextTestCase))]
        public void ExtractPlainText(string fileName)
        {
            string inputPath = fileName.ToInput();
            string outputName = $"ExtractPlainText_{fileName}.txt";
            string outputPath = outputName.ToOutput();

            PdfTextExtractor.Extract(inputPath, false, outputPath);
            CompareText(outputName);
        }

        [TestCaseSource(nameof(ExtractTextTestCase))]
        public void ExtractFormattedText(string fileName)
        {
            string inputPath = fileName.ToInput();
            string outputName = $"ExtractFormattedText_{fileName}.txt";
            string outputPath = outputName.ToOutput();

            PdfTextExtractor.Extract(inputPath, true, outputPath);
            CompareText(outputName);
        }

        public static void CompareText(string outputName)
        {
            string outputPath = outputName.ToOutput();
            FileAssert.AreEqual(outputPath, outputName.ToExpected());
            File.Delete(outputPath);
        }

        public static readonly object[] ExtractTextTestCase =
        {
            new object[] { "DocumentWithWatermark.pdf" },
            new object[] { "gmail-cheat-sheet.pdf" },
            new object[] { "shipping-request.pdf" },
        };
    }
}
