using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class OptimizeImages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            const string originalFile = @"Sample Data\jpeg.pdf";
            const string compressedFile = "OptimizeImages.pdf";

            using (PdfDocument pdf = new PdfDocument(originalFile))
            {
                var alreadyCompressedImageIds = new HashSet<string>();
                for (int i = 0; i < pdf.PageCount; ++i)
                {
                    PdfPage page = pdf.Pages[i];
                    foreach (PdfPaintedImage painted in page.GetPaintedImages())
                    {
                        PdfImage image = painted.Image;
                        if (alreadyCompressedImageIds.Contains(image.Id))
                            continue;

                        if (optimizeImage(painted, pdf))
                            alreadyCompressedImageIds.Add(image.Id);
                    }
                }

                pdf.Save(compressedFile);
            }

            // NOTE: 
            // This sample shows only one approach to reduce size of a PDF.
            // Please check CompressAllTechniques sample code to see more approaches.
            // https://github.com/BitMiracle/Docotic.Pdf.Samples/tree/master/Samples/Compression/CompressAllTechniques

            string message = string.Format(
                "Original file size: {0} bytes;\r\nCompressed file size: {1} bytes",
                new FileInfo(originalFile).Length,
                new FileInfo(compressedFile).Length
            );
            MessageBox.Show(message);

            Process.Start(compressedFile);
        }

        private static bool optimizeImage(PdfPaintedImage painted, PdfDocument pdf)
        {
            PdfImage image = painted.Image;

            // inline images can not be recompressed unless you move them to resources
            // using PdfCanvas.MoveInlineImagesToResources 
            if (image.IsInline)
                return false;

            // mask images are not good candidates for recompression
            if (image.IsMask || image.Width < 8 || image.Height < 8)
                return false;

            // get size of the painted image
            int width = Math.Max(1, (int)painted.Bounds.Width);
            int height = Math.Max(1, (int)painted.Bounds.Height);
            if (width >= image.Width || height >= image.Height)
            {
                if (image.HasMask)
                    return false;

                if (image.ComponentCount == 1 && image.BitsPerComponent == 1 &&
                    image.Compression != PdfImageCompression.Group4Fax)
                {
                    image.RecompressWithGroup4Fax();
                }
                else if (image.BitsPerComponent == 8 &&
                    image.ComponentCount >= 3 &&
                    image.Compression != PdfImageCompression.Jpeg &&
                    image.Compression != PdfImageCompression.Jpeg2000)
                {
                    image.RecompressWithJpeg2000(10);
                    // or image.RecompressWithJpeg();
                }

                return true;
            }

            // try to replace large masked images
            if (image.HasMask)
            {
                if (image.Width < 300 || image.Height < 300)
                    return false;

                using (var stream = new MemoryStream())
                {
                    PdfPage tempPage = pdf.AddPage();
                    tempPage.Canvas.DrawImage(image, 0, 0, width, height, 0);
                    PdfPaintedImage tempPaintedImage = tempPage.GetPaintedImages()[0];

                    tempPaintedImage.SaveAsPainted(stream);

                    pdf.RemovePage(pdf.PageCount - 1);

                    image.ReplaceWith(stream);
                    return true;
                }
            }

            if (image.Compression == PdfImageCompression.Group4Fax ||
                image.Compression == PdfImageCompression.Group3Fax)
            {
                // Fax documents usually looks better if integer-ratio scaling is used
                // Fractional-ratio scaling introduces more artifacts
                int ratio = Math.Min(image.Width / width, image.Height / height);

                // decrease the ratio when it is too high
                if (ratio > 6)
                    ratio -= 5;
                else if (ratio > 3)
                    ratio -= 2;

                image.ResizeTo(image.Width / ratio, image.Height / ratio, PdfImageCompression.Group4Fax);
            }
            else if (image.ComponentCount >= 3 && image.BitsPerComponent == 8)
            {
                image.ResizeTo(width, height, PdfImageCompression.Jpeg, 90);
            }
            else
            {
                image.ResizeTo(width, height, PdfImageCompression.Flate, 9);
            }

            return true;
        }
    }
}
