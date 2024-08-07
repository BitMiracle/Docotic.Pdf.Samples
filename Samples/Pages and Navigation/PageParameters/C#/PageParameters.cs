using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class PageParameters
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "PageParameters.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.InitialView = pdf.CreateView(0);
                pdf.InitialView.SetFitPage();

                PdfPage firstPage = pdf.Pages[0];
                firstPage.Size = PdfPaperSize.A6;

                PdfPage secondPage = pdf.AddPage();
                secondPage.Width = 300;
                secondPage.Height = 100;

                PdfPage thirdPage = pdf.AddPage();
                thirdPage.Rotation = PdfRotation.Rotate90;

                PdfPage fourthPage = pdf.AddPage();
                fourthPage.Orientation = PdfPaperOrientation.Landscape;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
