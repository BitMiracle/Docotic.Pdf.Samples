using System;
using System.Collections.Generic;
using System.IO;

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

            string originalFile = @"..\Sample Data\jpeg.pdf";
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

                        if (optimizeImage(painted))
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
            Console.WriteLine(message);
            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        private static bool optimizeImage(PdfPaintedImage painted)
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

            // calculate resize ratio
            double ratio = Math.Min(image.Width / (double)width, image.Height / (double)height);

            if (ratio <= 1)
            {
                // the image size is smaller then the painted size
                return recompressImage(image);
            }

            if (ratio < 1.1)
            {
                // with ratio this small, the potential size reduction
                // usually does not justify resizing artefacts
                return false;
            }

            if (image.Compression == PdfImageCompression.Group4Fax ||
                image.Compression == PdfImageCompression.Group3Fax ||
                image.Compression == PdfImageCompression.JBig2 ||
                (image.ComponentCount == 1 && image.BitsPerComponent == 1))
            {
                return resizeBilevelImage(image, ratio);
            }

            int resizedWidth = (int)Math.Floor(image.Width / ratio);
            int resizedHeight = (int)Math.Floor(image.Height / ratio);
            if ((image.ComponentCount >= 3 && image.BitsPerComponent == 8) || isGrayJpeg(image))
            {
                image.ResizeTo(resizedWidth, resizedHeight, PdfImageCompression.Jpeg, 90);
                // or image.ResizeTo(resizedWidth, resizedHeight, PdfImageCompression.Jpeg2000, 10);
            }
            else
            {
                image.ResizeTo(resizedWidth, resizedHeight, PdfImageCompression.Flate, 9);
            }

            return true;
        }

        private static bool recompressImage(PdfImage image)
        {
            if (image.ComponentCount == 1 &&
                image.BitsPerComponent == 1 &&
                image.Compression == PdfImageCompression.Group3Fax)
            {
                image.RecompressWithGroup4Fax();
                return true;
            }

            if (image.BitsPerComponent == 8 &&
                image.ComponentCount >= 3 &&
                image.Compression != PdfImageCompression.Jpeg &&
                image.Compression != PdfImageCompression.Jpeg2000)
            {
                if (image.Width < 64 && image.Height < 64)
                {
                    // JPEG better preserves detail on smaller images 
                    image.RecompressWithJpeg();
                }
                else
                {
                    // you can try larger compressio ratio for bigger images
                    image.RecompressWithJpeg2000(10);
                }

                return true;
            }

            return false;
        }

        private static bool resizeBilevelImage(PdfImage image, double ratio)
        {
            // Fax documents usually look better if integer-ratio scaling is used
            // Fractional-ratio scaling introduces more artifacts
            int intRatio = (int)ratio;

            // decrease the ratio when it is too high
            if (intRatio > 3)
                intRatio = Math.Min(intRatio - 2, 3);

            if (intRatio == 1 && image.Compression == PdfImageCompression.Group4Fax)
            {
                // skipping the image, because the output size and compression are the same
                return false;
            }

            image.ResizeTo(image.Width / intRatio, image.Height / intRatio, PdfImageCompression.Group4Fax);
            return true;
        }

        private static bool isGrayJpeg(PdfImage image)
        {
            var isJpegCompressed = image.Compression == PdfImageCompression.Jpeg ||
                image.Compression == PdfImageCompression.Jpeg2000;
            return isJpegCompressed && image.ComponentCount == 1 && image.BitsPerComponent == 8;
        }
    }
}
