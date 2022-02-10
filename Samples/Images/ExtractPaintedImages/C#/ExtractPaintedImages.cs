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
                image.Image.Save("PdfImage.Save");

                // save image as painted
                var options = new PdfPaintedImageSavingOptions
                {
                    Format = PdfExtractedImageFormat.Tiff,
                    HorizontalResolution = 300,
                    VerticalResolution = 300
                };
                image.SaveAsPainted("PdfPaintedImage.SaveAsPainted.tiff", options);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
