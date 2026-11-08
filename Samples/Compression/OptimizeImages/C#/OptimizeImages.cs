using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class OptimizeImages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string originalFile = @"..\Sample Data\jpeg.pdf";
            const string compressedFile = "OptimizeImages.pdf";

            using (var pdf = new PdfDocument(originalFile))
            {
                var alreadyCompressedImageIds = new HashSet<string>();
                for (int i = 0; i < pdf.Pages.Count; ++i)
                {
                    PdfPage page = pdf.Pages[i];
                    foreach (PdfPaintedImage painted in page.GetPaintedImages())
                    {
                        PdfImage image = painted.Image;

                        // Inline images have no ID and cannot be recompressed
                        if (image.Id == null)
                            continue;

                        if (alreadyCompressedImageIds.Contains(image.Id))
                            continue;

                        if (OptimizeImage(painted))
                            alreadyCompressedImageIds.Add(image.Id);
                    }
                }

                pdf.Save(compressedFile);
            }

            // NOTE:
            // This sample demonstrates one approach to reducing PDF file size.
            // See the CompressAllTechniques sample for additional compression techniques:
            // https://github.com/BitMiracle/Docotic.Pdf.Samples/tree/master/Samples/Compression/CompressAllTechniques

            string message = string.Format(
                "Original file size: {0} bytes;\r\nCompressed file size: {1} bytes",
                new FileInfo(originalFile).Length,
                new FileInfo(compressedFile).Length
            );
            Console.WriteLine(message);
            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(compressedFile) { UseShellExecute = true });
        }

        private static bool OptimizeImage(PdfPaintedImage painted)
        {
            PdfImage image = painted.Image;

            // Inline images cannot be recompressed unless they are first moved to page resources
            // using PdfCanvas.MoveInlineImagesToResources.
            if (image.IsInline)
                return false;

            // Skip mask images and very small images, as they are poor candidates for
            // recompression.
            if (image.IsMask || image.Width < 8 || image.Height < 8)
                return false;

            // Get the dimensions of the painted image
            int width = Math.Max(1, (int)painted.Bounds.Width);
            int height = Math.Max(1, (int)painted.Bounds.Height);

            // Calculate the ratio between the original image size and the painted size.
            double ratio = Math.Min(image.Width / (double)width, image.Height / (double)height);

            if (ratio <= 1)
            {
                // The image is no larger than its painted size, so recompress it without resizing.
                return RecompressImage(image);
            }

            if (ratio < 1.1)
            {
                // The image is only slightly larger than its painted size.
                // Resizing is unlikely to reduce the file size enough to justify the quality loss.
                return false;
            }

            if (image.Compression == PdfImageCompression.Group4Fax ||
                image.Compression == PdfImageCompression.Group3Fax ||
                image.Compression == PdfImageCompression.JBig2 ||
                (image.ComponentCount == 1 && image.BitsPerComponent == 1))
            {
                return ResizeBilevelImage(image, ratio);
            }

            int resizedWidth = (int)Math.Floor(image.Width / ratio);
            int resizedHeight = (int)Math.Floor(image.Height / ratio);
            if ((image.ComponentCount >= 3 && image.BitsPerComponent == 8) || IsGrayJpeg(image))
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

        private static bool RecompressImage(PdfImage image)
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
                    // JPEG preserves fine detail better in smaller images.
                    image.RecompressWithJpeg();
                }
                else
                {
                    // Larger images can often tolerate more aggressive compression.
                    image.RecompressWithJpeg2000(10);
                }

                return true;
            }

            return false;
        }

        private static bool ResizeBilevelImage(PdfImage image, double ratio)
        {
            // Fax documents usually look better when scaled by an integer factor.
            // Fractional scaling tends to introduce more artifacts.
            int intRatio = (int)ratio;

            // Limit the downscaling factor to avoid excessive quality loss.
            if (intRatio > 3)
                intRatio = Math.Min(intRatio - 2, 3);

            if (intRatio == 1 && image.Compression == PdfImageCompression.Group4Fax)
            {
                // No resize is needed and the image already uses the target compression.
                return false;
            }

            image.ResizeTo(image.Width / intRatio, image.Height / intRatio, PdfImageCompression.Group4Fax);
            return true;
        }

        private static bool IsGrayJpeg(PdfImage image)
        {
            var isJpegCompressed = image.Compression == PdfImageCompression.Jpeg ||
                image.Compression == PdfImageCompression.Jpeg2000;
            return isJpegCompressed && image.ComponentCount == 1 && image.BitsPerComponent == 8;
        }
    }
}
