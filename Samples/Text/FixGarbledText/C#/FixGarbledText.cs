using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Tesseract;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class FixGarbledText
    {
        public static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            var documentText = new StringBuilder();
            using (var pdf = new PdfDocument(@"..\Sample data\Broken text encoding.pdf"))
            {
                string? location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (location == null)
                {
                    Console.WriteLine("Invalid assembly location");
                    return;
                }

                string tessData = Path.Combine(location, @"tessdata");
                using var engine = new TesseractEngine(tessData, "eng", EngineMode.LstmOnly);

                for (int i = 0; i < pdf.PageCount; ++i)
                {
                    if (documentText.Length > 0)
                        documentText.Append("\r\n\r\n");

                    // Get a list of character codes that are not mapped to Unicode correctly
                    var unmappedCharacterCodes = new List<PdfCharacterCode>();
                    var firstPassOptions = new PdfTextExtractionOptions
                    {
                        UnmappedCharacterCodeHandler = c =>
                        {
                            unmappedCharacterCodes.Add(c);
                            return null;
                        }
                    };
                    PdfPage page = pdf.Pages[i];
                    string text = page.GetText(firstPassOptions);
                    if (unmappedCharacterCodes.Count == 0)
                    {
                        // There are no unmapped characters. Use the extracted text as is
                        documentText.Append(text);
                        continue;
                    }

                    // Perform OCR to get correct Unicode values
                    string[] recognizedText = OcrCharacterCodes(unmappedCharacterCodes, engine);

                    // Extract text replacing unmapped characters with correct Unicode values
                    int index = 0;
                    var options = new PdfTextExtractionOptions
                    {
                        UnmappedCharacterCodeHandler = c =>
                        {
                            string t = recognizedText[index];
                            ++index;
                            return t ?? " ";
                        }
                    };
                    documentText.Append(page.GetText(options));
                }
            }

            var pathToFile = "FixGarbledText.txt";
            using (var writer = new StreamWriter(pathToFile))
                writer.Write(documentText.ToString());

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static string[] OcrCharacterCodes(List<PdfCharacterCode> charCodes, TesseractEngine engine)
        {
            string[] recognizedText = new string[charCodes.Count];

            var rasterizer = new PdfTextRasterizer
            {
                HorizontalResolution = 300,
                VerticalResolution = 300,
                Height = 20,

                // Slightly increase distance between characters to improve OCR quality
                CharacterSpacing = 1.5
            };

            // Split character codes to batches because Tesseract cannot process too wide images.
            // You may find the appropriate batch size heuristically based on the output image height and resolution.
            // Or you may rasterize all character codes with the PdfTextRasterizer.Save method and
            // calculate the batch size using the returning widths.
            const int BatchSize = 400;
            int batchIndex = 0;
            foreach (PdfCharacterCode[] batchCodes in charCodes.Chunk(BatchSize))
            {
                using (var charCodeImage = new MemoryStream())
                {
                    // Get bounds of rendered character codes
                    double[] widthsPoints = rasterizer.Save(charCodeImage, batchCodes);
                    double[] positionsPoints = new double[widthsPoints.Length];
                    for (int j = 1; j < positionsPoints.Length; ++j)
                        positionsPoints[j] = positionsPoints[j - 1] + widthsPoints[j - 1] + rasterizer.CharacterSpacing;

                    // Perform OCR
                    using Pix img = Pix.LoadFromMemory(charCodeImage.ToArray());

                    // Use SingleChar segmentation mode when the last batch contains a few characters
                    PageSegMode segMode = batchCodes.Length > 5 ? engine.DefaultPageSegMode : PageSegMode.SingleChar;
                    using Page recognizedPage = engine.Process(img, segMode);
                    using ResultIterator iter = recognizedPage.GetIterator();

                    // Map character codes to the recognized text
                    int lastCharCodeIndex = -1;
                    const PageIteratorLevel Level = PageIteratorLevel.Symbol;
                    iter.Begin();
                    do
                    {
                        if (iter.TryGetBoundingBox(Level, out Rect bounds))
                        {
                            double bestIntersectionWidth = 0;
                            int bestMatchIndex = -1;
                            for (int c = lastCharCodeIndex + 1; c < batchCodes.Length; ++c)
                            {
                                double x = PointsToPixels(positionsPoints[c], rasterizer.HorizontalResolution);
                                double width = PointsToPixels(widthsPoints[c], rasterizer.HorizontalResolution);

                                if (bounds.X2 < x)
                                    break;

                                if (x + width < bounds.X1)
                                    continue;

                                double intersection = Math.Min(bounds.X2, x + width) - Math.Max(bounds.X1, x);
                                if (bestIntersectionWidth < intersection)
                                {
                                    bestIntersectionWidth = intersection;
                                    bestMatchIndex = c;
                                }
                            }

                            if (bestMatchIndex >= 0)
                            {
                                recognizedText[batchIndex * BatchSize + bestMatchIndex] = iter.GetText(Level);
                                lastCharCodeIndex = bestMatchIndex;
                            }
                        }
                    } while (iter.Next(Level));
                }

                ++batchIndex;
            }

            return recognizedText;
        }

        private static double PointsToPixels(double points, double dpi)
        {
            return points * dpi / 72;
        }
    }
}