using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using Tesseract;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class OcrAndMakeSearchable
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (var pdf = new PdfDocument(@"..\Sample data\Freedman Scora.pdf"))
            {
                // This font is used to draw all recognized text chunks in PDF.
                // Make sure that the font defines all glyphs for the target language.
                PdfFont universalFont = pdf.AddFont("Arial");

                var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var tessData = Path.Combine(location, @"tessdata");
                using (var engine = new TesseractEngine(tessData, "eng", EngineMode.LstmOnly))
                {
                    for (int i = 0; i < pdf.PageCount; ++i)
                    {
                        PdfPage page = pdf.Pages[i];

                        // Simple check if the page contains searchable text.
                        // We do not need to do OCR in that case.
                        if (!string.IsNullOrEmpty(page.GetText().Trim()))
                            continue;

                        var canvas = page.Canvas;
                        canvas.Font = universalFont;

                        // Produce invisible, but searchable text
                        canvas.TextRenderingMode = PdfTextRenderingMode.NeitherFillNorStroke;

                        const int Dpi = 200;
                        const double ImageToPdfScaleFactor = 72.0 / Dpi;
                        foreach (RecognizedTextChunk word in recognizeWords(page, engine, Dpi, $"page_{i}.png"))
                        {
                            if (word.Confidence < 80)
                                Console.WriteLine($"Possible recognition error: low confidence {word.Confidence} for word '{word.Text}'");

                            Rect bounds = word.Bounds;
                            PdfRectangle pdfBounds = new PdfRectangle(
                                bounds.X1 * ImageToPdfScaleFactor,
                                bounds.Y1 * ImageToPdfScaleFactor,
                                bounds.Width * ImageToPdfScaleFactor,
                                bounds.Height * ImageToPdfScaleFactor
                            );

                            tuneFontSize(canvas, pdfBounds.Width, word.Text);

                            double distanceToBaseLine = getDistanceToBaseline(canvas.Font, canvas.FontSize);
                            var position = new PdfPoint(pdfBounds.Left, pdfBounds.Bottom - distanceToBaseLine);
                            showTextAtRotatedPage(word.Text, position, page, page.MediaBox);
                        }
                    }
                }

                universalFont.RemoveUnusedGlyphs();

                const string Result = "OcrAndMakeSearchable.pdf";
                pdf.Save(Result);

                Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

                Process.Start(new ProcessStartInfo(Result) { UseShellExecute = true });
            }
        }

        private static IEnumerable<RecognizedTextChunk> recognizeWords(PdfPage page, TesseractEngine engine,
            int resolution, string tempFileName)
        {
            // Save PDF page as high-resolution image
            PdfDrawOptions options = PdfDrawOptions.Create();
            options.BackgroundColor = new PdfRgbColor(255, 255, 255);
            options.HorizontalResolution = resolution;
            options.VerticalResolution = resolution;
            page.Save(tempFileName, options);

            using (var img = Pix.LoadFromFile(tempFileName))
            {
                using (var recognizedPage = engine.Process(img))
                {
                    using (ResultIterator iter = recognizedPage.GetIterator())
                    {
                        const PageIteratorLevel Level = PageIteratorLevel.Word;
                        iter.Begin();
                        do
                        {
                            if (iter.TryGetBoundingBox(Level, out Rect bounds))
                            {
                                string text = iter.GetText(Level);
                                float confidence = iter.GetConfidence(Level);

                                yield return new RecognizedTextChunk(text, bounds, confidence);
                            }
                        } while (iter.Next(Level));
                    }
                }
            }
        }

        private static void tuneFontSize(PdfCanvas canvas, double targetTextWidth, string text)
        {
            const double Step = 0.1;

            double bestFontSize = canvas.FontSize;
            double bestDiff = targetTextWidth - canvas.GetTextWidth(text);
            while (true)
            {
                int diffSign = Math.Sign(bestDiff);
                if (diffSign == 0)
                    break;

                // try neigbour font size
                canvas.FontSize += diffSign * Step;

                double newDiff = targetTextWidth - canvas.GetTextWidth(text);
                if (Math.Abs(newDiff) >= Math.Abs(bestDiff))
                {
                    canvas.FontSize = bestFontSize;
                    break;
                }

                bestDiff = newDiff;
                bestFontSize = canvas.FontSize;
            }
        }

        private static double getDistanceToBaseline(PdfFont font, double fontSize)
        {
            return font.TopSideBearing * font.TransformationMatrix.M22 * fontSize;
        }

        private static void showTextAtRotatedPage(string text, PdfPoint position, PdfPage page, PdfBox pageBox)
        {
            PdfCanvas canvas = page.Canvas;
            if (page.Rotation == PdfRotation.None)
            {
                canvas.DrawString(position.X, position.Y, text);
                return;
            }

            canvas.SaveState();

            switch (page.Rotation)
            {
                case PdfRotation.Rotate90:
                    canvas.TranslateTransform(position.Y, pageBox.Height - position.X);
                    break;

                case PdfRotation.Rotate180:
                    canvas.TranslateTransform(pageBox.Width - position.X, pageBox.Height - position.Y);
                    break;

                case PdfRotation.Rotate270:
                    canvas.TranslateTransform(pageBox.Width - position.Y, position.X);
                    break;
            }
            canvas.RotateTransform((int)page.Rotation * 90);
            canvas.DrawString(0, 0, text);

            canvas.RestoreState();
        }

        private class RecognizedTextChunk
        {
            public RecognizedTextChunk(string text, Rect bounds, float confidence)
            {
                Text = text;
                Bounds = bounds;
                Confidence = confidence;
            }

            public string Text { get; }
            public Rect Bounds { get; }
            public float Confidence { get; }
        }
    }
}