using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class UriAction
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var outputName = "UriAction.pdf";
            using (var pdf = new PdfDocument())
            {
                PdfUriAction uriAction = pdf.CreateHyperlinkAction(new Uri("http://www.google.com"));
                PdfActionArea annotation = pdf.Pages[0].AddActionArea(10, 50, 100, 100, uriAction);
                annotation.Border.Color = new PdfRgbColor(0, 0, 0);
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
