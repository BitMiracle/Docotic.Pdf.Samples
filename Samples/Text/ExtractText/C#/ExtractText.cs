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
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument("Sample data/jfif3.pdf"))
            {
                // Extract plain text from document
                string documentTextFile = "Document text.txt";
                using (StreamWriter writer = new StreamWriter(documentTextFile))
                    writer.Write(pdf.GetText());

                Process.Start(documentTextFile);

                // Extract text with formatting from document
                string documentTextFormattedFile = "Document text with formatting.txt";
                using (StreamWriter writer = new StreamWriter(documentTextFormattedFile))
                    writer.Write(pdf.GetTextWithFormatting());

                Process.Start(documentTextFormattedFile);

                // Only extract visible plain text from first page
                string firstPageTextFile = "First page text.txt";
                using (StreamWriter writer = new StreamWriter(firstPageTextFile))
                {
                    var options = new PdfTextExtractionOptions
                    {
                        WithFormatting = false,
                        SkipInvisibleText = true
                    };
                    writer.Write(pdf.Pages[0].GetText(options));
                }

                Process.Start(firstPageTextFile);
            }
        }
    }
}