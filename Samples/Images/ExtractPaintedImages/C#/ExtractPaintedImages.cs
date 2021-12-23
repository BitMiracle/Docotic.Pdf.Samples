using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractPaintedImages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument(@"..\Sample Data\ImageScaleAndRotate.pdf"))
            {
                PdfCollection<PdfPaintedImage> paintedImages = pdf.Pages[0].GetPaintedImages();
                
                PdfPaintedImage image = paintedImages[0];

                // save image as is
                string imageAsIs = "PdfImage.Save";
                image.Image.Save(imageAsIs);

                // save image as painted
                string imageAsPainted = "PdfPaintedImage.SaveAsPainted.tiff";
                image.SaveAsPainted(imageAsPainted, PdfExtractedImageFormat.Tiff);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
