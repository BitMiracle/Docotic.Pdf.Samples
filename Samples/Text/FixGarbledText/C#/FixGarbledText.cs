using System;
using System.Collections.Generic;
using System.IO;
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
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var documentText = new StringBuilder();
            using (var pdf = new PdfDocument(@"..\Sample data\Broken text encoding.pdf"))
            {
                string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string tessData = Path.Combine(location, @"tessdata");
                using (var engine = new TesseractEngine(tessData, "eng", EngineMode.LstmOnly))
                {
                    for (int i = 0; i < pdf.PageCount; ++i)
                    {
                        if (documentText.Length > 0)
                            documentText.Append("\r\n\r\n");

                        // get a list of character codes that are not mapped to Unicode correctly
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
                            // there are no unmapped characters. Use the extracted text as is
                            documentText.Append(text);
                            continue;
                        }

                        // perform OCR to get correct Unicode values
                        string[] recognizedText = ocrCharacterCodes(unmappedCharacterCodes, engine);

                        // extract text replacing unmapped characters with correct Unicode values
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
            }

            using (var writer = new StreamWriter("FixGarbledText.txt"))
                writer.Write(documentText.ToString());

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        private static string[] ocrCharacterCodes(List<PdfCharacterCode> charCodes, TesseractEngine engine)
        {
            string[] recognizedText = new string[charCodes.Count];

            var rasterizer = new PdfTextRasterizer
            {
                HorizontalResolution = 300,
                VerticalResolution = 300,
                Height = 20,

                // slightly increase distance between characters to improve OCR quality
                CharacterSpacing = 1.5 
            };
            using (var charCodeImage = new MemoryStream())
            {
                // get bounds of rendered character codes
                double[] widthsPoints = rasterizer.Save(charCodeImage, charCodes);
                double[] positionsPoints = new double[widthsPoints.Length];
                for (int j = 1; j < positionsPoints.Length; ++j)
                    positionsPoints[j] = positionsPoints[j - 1] + widthsPoints[j - 1] + rasterizer.CharacterSpacing;

                // perform OCR
                using (Pix img = Pix.LoadFromMemory(charCodeImage.ToArray()))
                {
                    using (Page recognizedPage = engine.Process(img))
                    {
                        using (ResultIterator iter = recognizedPage.GetIterator())
                        {
                            // map character codes to the recognized text
                            int lastCharCodeIndex = -1;
                            const PageIteratorLevel Level = PageIteratorLevel.Symbol;
                            iter.Begin();
                            do
                            {
                                if (iter.TryGetBoundingBox(Level, out Rect bounds))
                                {
                                    double bestIntersectionWidth = 0;
                                    int bestMatchIndex = -1;
                                    for (int c = lastCharCodeIndex + 1; c < charCodes.Count; ++c)
                                    {
                                        double x = pointsToPixels(positionsPoints[c], rasterizer.HorizontalResolution);
                                        double width = pointsToPixels(widthsPoints[c], rasterizer.HorizontalResolution);

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
                                        recognizedText[bestMatchIndex] = iter.GetText(Level);
                                        lastCharCodeIndex = bestMatchIndex;
                                    }
                                }
                            } while (iter.Next(Level));
                        }
                    }
                }
            }

            return recognizedText;
        }

        private static double pointsToPixels(double points, double dpi)
        {
            return points * dpi / 72;
        }
    }
}