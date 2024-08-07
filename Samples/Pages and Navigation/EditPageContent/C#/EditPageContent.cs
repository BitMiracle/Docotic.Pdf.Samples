using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class EditPageContent
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "EditPageContent.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                foreach (PdfPage page in pdf.Pages)
                {
                    var editor = new PageContentEditor(pdf, null, ReplaceColor, ShouldRemoveText);
                    editor.Edit(page);
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        /// <summary>
        /// Replaces orange color with gray.
        /// </summary>
        private static PdfColor ReplaceColor(PdfColor color)
        {
            if (color is PdfRgbColor rgb)
            {
                if (rgb.R == 255 && rgb.G == 127 && rgb.B == 64)
                    return new PdfGrayColor(70);
            }

            return color;
        }

        /// <summary>
        /// Removes "(Max...)" text on the right.
        /// </summary>
        private static bool ShouldRemoveText(PdfTextData text)
        {
            return text.Bounds.X > 200;
        }
    }
}