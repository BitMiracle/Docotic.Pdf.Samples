using System.Collections.Generic;
using System.Diagnostics;
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

            using (var pdf = new PdfDocument("Sample data/Freedman Scora.pdf"))
            {
                // This font is used to draw all recognized text chunks in PDF.
                // Make sure that the font defines all glyphs for the target language.
                PdfFont universalFont = pdf.AddFont("Arial");

                using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
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
                        canvas.TextRenderingMode = PdfTextRenderingMode.NeitherFillNorStroke;

                        const int Dpi = 600;
                        const double ImageToPdfScaleFactor = 72.0 / Dpi;
                        foreach (RecognizedTextChunk word in recognizeWords(page, engine, Dpi, $"page_{i}.png"))
                        {
                            Rect bounds = word.Bounds;
                            var position = new PdfPoint(
                                bounds.X1 * ImageToPdfScaleFactor,
                                bounds.Y1 * ImageToPdfScaleFactor
                            );
                            canvas.FontSize = bounds.Height * ImageToPdfScaleFactor;
                            canvas.DrawString(position, word.Text);
                        }
                    }
                }

                universalFont.RemoveUnusedGlyphs();

                const string Result = "OcrAndMakeSearchable.pdf";
                pdf.Save(Result);

                Process.Start(Result);
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
                        iter.Begin();
                        do
                        {
                            if (iter.TryGetBoundingBox(PageIteratorLevel.Word, out Rect bounds))
                            {
                                string text = iter.GetText(PageIteratorLevel.Word);
                                yield return new RecognizedTextChunk(text, bounds);
                            }
                        } while (iter.Next(PageIteratorLevel.Word));
                    }
                }
            }
        }

        private class RecognizedTextChunk
        {
            public RecognizedTextChunk(string text, Rect bounds)
            {
                Text = text;
                Bounds = bounds;
            }

            public string Text { get; }
            public Rect Bounds { get; }
        }
    }
}