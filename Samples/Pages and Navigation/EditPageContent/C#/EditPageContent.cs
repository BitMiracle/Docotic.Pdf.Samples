using System;
using System.Diagnostics;
using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class EditPageContent
    {
        public static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

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