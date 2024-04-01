using System;
using System.Diagnostics;
using System.IO;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractText
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string documentTextFile = "Document text.txt";
            string documentTextFormattedFile = "Document text with formatting.txt";
            string firstPageTextFile = "First page text.txt";

            using (var pdf = new PdfDocument(@"..\Sample data\jfif3.pdf"))
            {
                // Extract plain text from document
                using (var writer = new StreamWriter(documentTextFile))
                    writer.Write(pdf.GetText());

                // Extract text with formatting from document
                using (var writer = new StreamWriter(documentTextFormattedFile))
                    writer.Write(pdf.GetTextWithFormatting());

                // Only extract visible plain text from first page
                using (var writer = new StreamWriter(firstPageTextFile))
                {
                    var options = new PdfTextExtractionOptions
                    {
                        WithFormatting = false,
                        SkipInvisibleText = true
                    };
                    writer.Write(pdf.Pages[0].GetText(options));
                }
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(documentTextFile) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(documentTextFormattedFile) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(firstPageTextFile) { UseShellExecute = true });
        }
    }
}