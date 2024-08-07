using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ColorProfiles
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "ColorProfiles.pdf";
            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                PdfColorProfile colorProfile = pdf.AddColorProfile(@"..\Sample data\AdobeRGB1998.icc");
                canvas.Brush.Color = new PdfRgbColor(colorProfile, 20, 80, 240);
                canvas.DrawRectangle(new PdfRectangle(10, 50, 100, 70), PdfDrawMode.Fill);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}