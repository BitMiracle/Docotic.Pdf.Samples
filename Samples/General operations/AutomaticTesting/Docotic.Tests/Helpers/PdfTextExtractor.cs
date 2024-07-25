namespace BitMiracle.Docotic.Pdf.Samples.Tests.Helpers
{
    static class PdfTextExtractor
    {
        public static void Extract(string inputPath, bool withFormatting, string outputPath)
        {
            using var pdf = new PdfDocument(inputPath);

            var options = new PdfTextExtractionOptions
            {
                WithFormatting = withFormatting,
            };
            string text = pdf.GetText(options);
            File.WriteAllText(outputPath, text);
        }
    }
}
