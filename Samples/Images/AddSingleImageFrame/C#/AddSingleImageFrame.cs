using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddSingleImageFrame
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "AddSingleImageFrame.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfImageFrames frames = pdf.OpenImage(@"..\Sample Data\multipage.tif");
                PdfImageFrame secondFrame = frames[1];

                PdfImage image = pdf.CreateImage(secondFrame);
                pdf.Pages[0].Canvas.DrawImage(image, 10, 10, 0);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
