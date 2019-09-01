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
                PdfFont universalFont = pdf.AddFont("Arial");
                using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
                {
                    for (int i = 0; i < pdf.PageCount; ++i)
                    {
                        PdfPage page = pdf.Pages[i];
                        string searchableText = page.GetText();

                        // Simple check if the page contains searchable text.
                        // We do not need to do OCR in that case.
                        if (!string.IsNullOrEmpty(searchableText.Trim()))
                            continue;

                        // Save PDF page as high-resolution image
                        PdfDrawOptions options = PdfDrawOptions.Create();
                        options.BackgroundColor = new PdfRgbColor(255, 255, 255);

                        const int Dpi = 600;
                        options.HorizontalResolution = Dpi;
                        options.VerticalResolution = Dpi;

                        string pageImage = $"page_{i}.png";
                        page.Save(pageImage, options);

                        var canvas = page.Canvas;
                        canvas.Font = universalFont;
                        canvas.TextRenderingMode = PdfTextRenderingMode.NeitherFillNorStroke;

                        const double ImageToPdfScaleFactor = 72.0 / Dpi;
                        using (var img = Pix.LoadFromFile(pageImage))
                        {
                            using (var recognizedPage = engine.Process(img))
                            {
                                using (ResultIterator iter = recognizedPage.GetIterator())
                                {
                                    iter.Begin();
                                    do
                                    {
                                        if (iter.TryGetBoundingBox(PageIteratorLevel.Word, out Rect wordBounds))
                                        {
                                            string wordText = iter.GetText(PageIteratorLevel.Word);

                                            var position = new PdfPoint(
                                                wordBounds.X1 * ImageToPdfScaleFactor,
                                                wordBounds.Y1 * ImageToPdfScaleFactor
                                            );
                                            canvas.FontSize = wordBounds.Height * ImageToPdfScaleFactor;
                                            canvas.DrawString(position, wordText);
                                        }
                                    } while (iter.Next(PageIteratorLevel.Word));
                                }
                            }
                        }
                    }
                }

                universalFont.RemoveUnusedGlyphs();

                const string Result = "OcrAndMakeSearchable.pdf";
                pdf.Save(Result);

                Process.Start(Result);
            }
        }
    }
}