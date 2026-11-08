using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TiffToPdf
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "TiffToPdf.pdf";

            using (var pdf = new PdfDocument())
            {
                string[] tiffFiles =
                [
                    @"..\Sample Data\multipage.tif",
                    @"..\Sample Data\pinkMask.tif"
                ];

                int imagesAdded = 0;
                foreach (string tiff in tiffFiles)
                {
                    // Open a TIFF image that may contain multiple pages.
                    PdfImageFrames frames = pdf.OpenImage(tiff);
                    var pages = pdf.Pages;
                    foreach (PdfImageFrame frame in frames)
                    {
                        PdfPage pdfPage = (imagesAdded == 0)
                            ? pages[0]
                            : pages.Add();

                        PdfImage image = pdf.CreateImage(frame);

                        // Set the page size to match the image size so that the image covers the
                        // entire page.
                        pdfPage.Height = image.Height;
                        pdfPage.Width = image.Width;
                        pdfPage.Canvas.DrawImage(image, 0, 0, pdfPage.Width, pdfPage.Height, 0);

                        imagesAdded++;
                    }
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
