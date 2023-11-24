using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TiffToPdf
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var pathToFile = "TiffToPdf.pdf";

            using (var pdf = new PdfDocument())
            {
                string[] tiffFiles =
                {
                    @"..\Sample Data\multipage.tif",
                    @"..\Sample Data\pinkMask.tif"
                };

                int imagesAdded = 0;
                foreach (string tiff in tiffFiles)
                {
                    // open potentially multipage TIFF
                    PdfImageFrames? frames = pdf.OpenImage(tiff);
                    if (frames is null)
                    {
                        Console.WriteLine("Cannot add image");
                        return;
                    }

                    foreach (PdfImageFrame frame in frames)
                    {
                        if (imagesAdded != 0)
                            pdf.AddPage();

                        PdfImage image = pdf.AddImage(frame);
                        PdfPage pdfPage = pdf.Pages[pdf.PageCount - 1];

                        // adjust page size, so image fill occupy whole page
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
