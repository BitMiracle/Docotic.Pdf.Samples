using System.Diagnostics;

using BitMiracle.Docotic.Pdf;

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

            using (PdfDocument pdf = new PdfDocument(@"Sample Data\ImageScaleAndRotate.pdf"))
            {
                PdfCollection<PdfPaintedImage> paintedImages = pdf.Pages[0].GetPaintedImages();
                
                PdfPaintedImage image = paintedImages[0];

                // save image as is
                string imageAsIs = "PdfImage.Save";
                string fullPath = image.Image.Save(imageAsIs);
                Process.Start(fullPath);

                // save image as painted
                // NOTE: PdfPaintedImage.SaveAsPainted() method is not supported in version for .NET Standard
                string imageAsPainted = "PdfPaintedImage.SaveAsPainted.tiff";
                image.SaveAsPainted(imageAsPainted, PdfExtractedImageFormat.Tiff);
                Process.Start(imageAsPainted);
            }
        }
    }
}
