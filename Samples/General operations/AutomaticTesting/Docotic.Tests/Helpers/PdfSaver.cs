namespace BitMiracle.Docotic.Pdf.Samples.Tests.Helpers
{
    static class PdfSaver
    {
        public static void Save(string inputPath, string password, string outputPath)
        {
            var d = new PdfStandardDecryptionHandler(password);
            using var pdf = new PdfDocument(inputPath, d);

            var options = new PdfSaveOptions
            {
                UseObjectStreams = pdf.UsesObjectStreams,
                Linearize = pdf.IsLinearized
            };
            pdf.Save(outputPath, options);
        }
    }
}
