using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Metadata
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "Metadata.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.Info.Author = "Sample Browser application";
                pdf.Info.Subject = "Document metadata";
                pdf.Info.Title = "Custom title goes here";
                pdf.Info.Keywords = "pdf, Docotic.Pdf";

                PdfPage page = pdf.Pages[0];
                page.Canvas.DrawString(10, 50, "Check document properties");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
