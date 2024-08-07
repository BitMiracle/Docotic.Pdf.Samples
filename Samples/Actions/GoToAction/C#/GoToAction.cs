using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class GoToAction
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var outputName = "GoToAction.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage secondPage = pdf.AddPage();
                secondPage.Canvas.DrawString(10, 300, "Go-to action target");

                PdfGoToAction action = pdf.CreateGoToPageAction(1, 300);
                PdfActionArea annotation = pdf.Pages[0].AddActionArea(10, 50, 100, 30, action);
                annotation.Border.Color = new PdfRgbColor(255, 0, 0);
                annotation.Border.DashPattern = new PdfDashPattern(new float[] { 3, 2 });
                annotation.Border.Width = 1;
                annotation.Border.Style = PdfMarkerLineStyle.Dashed;

                pdf.Save(outputName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputName) { UseShellExecute = true });
        }
    }
}
