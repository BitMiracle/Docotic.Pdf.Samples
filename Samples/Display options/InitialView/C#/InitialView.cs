using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class InitialView
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            UseFitHeight();
            UsePercentZoom();

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        private static void UseFitHeight()
        {
            var outputName = "FitHeight.pdf";
            using (var pdf = new PdfDocument())
            {
                pdf.InitialView = pdf.CreateView(0);
                pdf.InitialView.SetFitHeight();

                pdf.Save(outputName);
            }

            Process.Start(new ProcessStartInfo(outputName) { UseShellExecute = true });
        }

        private static void UsePercentZoom()
        {
            var outputName = "Percent.pdf";
            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                pdf.InitialView = pdf.CreateView(0);
                pdf.InitialView.SetZoom(40);

                pdf.Save(outputName);
            }

            Process.Start(new ProcessStartInfo(outputName) { UseShellExecute = true });
        }
    }
}