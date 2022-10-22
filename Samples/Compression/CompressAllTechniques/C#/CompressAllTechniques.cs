using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CompressAllTechniques
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string originalFile = @"..\Sample Data\jpeg.pdf";
            const string compressedFile = "CompressAllTechniques.pdf";

            using (var pdf = new PdfDocument(originalFile))
            {
                // 1. Remove duplicate objects such as fonts and images
                pdf.ReplaceDuplicateObjects();

                // 2. Recompress images
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

                // 3. Setup save options
                var saveOptions = new PdfSaveOptions
                {
                    Compression = PdfCompression.Flate,
                    UseObjectStreams = true,
                    RemoveUnusedObjects = true,
                    OptimizeIndirectObjects = true,
                    WriteWithoutFormatting = true
                };

                // 4. Remove structure information
                pdf.RemoveStructureInformation();

                // 5. Flatten form fields 
                // Controls become uneditable after that
                pdf.FlattenControls();

                // 6. Clear metadata
                pdf.Metadata.Unembed();
                pdf.Info.Clear(false);

                // 7. Unembed fonts
                unembedFonts(pdf);

                // 8. Remove unused resources
                pdf.RemoveUnusedResources();

                // 9. Remove unused font glyphs
                pdf.RemoveUnusedFontGlyphs();

                // 10. Remove page-piece dictionaries
                pdf.RemovePieceInfo();

                pdf.Save(compressedFile, saveOptions);
            }

            string message = string.Format(
                "Original file size: {0} bytes;\r\nCompressed file size: {1} bytes",
                new FileInfo(originalFile).Length,
                new FileInfo(compressedFile).Length
            );
            Console.WriteLine(message);

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(compressedFile) { UseShellExecute = true });
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

        /// <summary>
        /// This method unembeds any font that is:
        /// * installed in the OS
        /// * or has its name included in the "always unembed" list
        /// * and its name is not included in the "always keep" list. 
        /// </summary>
        private static void unembedFonts(PdfDocument pdf)
        {
            string[] alwaysUnembedList = new string[] { "MyriadPro-Regular" };
            string[] alwaysKeepList = new string[] { "ImportantFontName", "AnotherImportantFontName" };

            foreach (PdfFont font in pdf.GetFonts())
            {
                if (!font.Embedded ||
                    font.EncodingName == "Built-In" ||
                    Array.Exists(alwaysKeepList, name => font.Name == name))
                {
                    continue;
                }

                if (font.Format == PdfFontFormat.TrueType || font.Format == PdfFontFormat.CidType2)
                {
                    SystemFontLoader loader = SystemFontLoader.Instance;
                    byte[] fontBytes = loader.Load(font.Name, font.Bold, font.Italic);
                    if (fontBytes != null)
                    {
                        font.Unembed();
                        continue;
                    }
                }

                if (Array.Exists(alwaysUnembedList, name => font.Name == name))
                    font.Unembed();
            }
        }
    }
}
