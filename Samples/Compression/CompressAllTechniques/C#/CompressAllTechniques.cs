using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using BitMiracle.Docotic.Pdf;

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

            const string originalFile = @"Sample Data\jpeg.pdf";
            const string compressedFile = "CompressAllTechniques.pdf";

            using (var pdf = new PdfDocument(originalFile))
            {
                // 1. Recompress images
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

                // 2. Setup save options
                pdf.SaveOptions.Compression = PdfCompression.Flate;
                pdf.SaveOptions.UseObjectStreams = true;
                pdf.SaveOptions.RemoveUnusedObjects = true;
                pdf.SaveOptions.OptimizeIndirectObjects = true;
                pdf.SaveOptions.WriteWithoutFormatting = true;

                // 3. Remove structure information
                pdf.RemoveStructureInformation();

                // 4. Flatten form fields 
                // Controls become uneditable after that
                pdf.FlattenControls();

                // 5. Clear metadata
                pdf.Metadata.Basic.Clear();
                pdf.Metadata.DublinCore.Clear();
                pdf.Metadata.MediaManagement.Clear();
                pdf.Metadata.Pdf.Clear();
                pdf.Metadata.RightsManagement.Clear();
                pdf.Metadata.Custom.Properties.Clear();

                foreach (XmpSchema schema in pdf.Metadata.Schemas)
                    schema.Properties.Clear();

                pdf.Info.Clear(false);

                // 6. Remove font duplicates
                pdf.ReplaceDuplicateFonts();

                // 7. Unembed fonts
                unembedFonts(pdf);

                // 8. Remove unused resources
                pdf.RemoveUnusedResources();

                // 9. Remove page-piece dictionaries
                pdf.RemovePieceInfo();

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
                    const double ResizeRatio = 1.4;

                    double newWidth = width / ResizeRatio;
                    double newHeight = height / ResizeRatio;

                    PdfPage tempPage = pdf.AddPage();
                    tempPage.Canvas.DrawImage(image, 0, 0, newWidth, newHeight, 0);
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
                    GdiFontLoader loader = new GdiFontLoader();
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
