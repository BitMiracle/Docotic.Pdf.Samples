using System;
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
                foreach (PdfPage page in pdf.Pages)
                {
                    foreach (PdfPaintedImage painted in page.GetPaintedImages())
                    {
                        PdfImage image = painted.Image;

                        // image that is used as mask or image with attached mask are
                        // not good candidates for recompression
                        if (image.IsMask || image.Mask != null || image.Width < 8 || image.Height < 8)
                            continue;

                        // get size of the painted image
                        int width = Math.Max(1, (int)painted.Bounds.Width);
                        int height = Math.Max(1, (int)painted.Bounds.Height);

                        if (image.ComponentCount == 1 && image.BitsPerComponent == 1 &&
                            image.Compression != PdfImageCompression.Group4Fax)
                        {
                            image.RecompressWithGroup4Fax();
                        }
                        else if (width < image.Width || height < image.Height)
                        {
                            // NOTE: PdfImage.ResizeTo() method is not supported in version for .NET Standard
                            if (image.ComponentCount >= 3)
                                image.ResizeTo(width, height, PdfImageCompression.Jpeg, 90);
                            else
                                image.ResizeTo(width, height, PdfImageCompression.Flate, 9);
                        }
                    }
                }

                pdf.Save(compressedFile);
            }

            string message = string.Format(
                "Original file size: {0} bytes;\r\nCompressed file size: {1} bytes",
                new FileInfo(originalFile).Length,
                new FileInfo(compressedFile).Length
            );
            MessageBox.Show(message);

            Process.Start(compressedFile);
        }
    }
}
