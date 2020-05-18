using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Tesseract;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class OcrAndExtractText
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var documentText = new StringBuilder();
            using (var pdf = new PdfDocument("Sample data/Freedman Scora.pdf"))
            {
                using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
                {
                    for (int i = 0; i < pdf.PageCount; ++i)
                    {
                        if (documentText.Length > 0)
                            documentText.Append("\r\n\r\n");

                        PdfPage page = pdf.Pages[i];
                        string searchableText = page.GetText();

                        // Simple check if the page contains searchable text.
                        // We do not need to do OCR in that case.
                        if (!string.IsNullOrEmpty(searchableText.Trim()))
                        {
                            documentText.Append(searchableText);
                            continue;
                        }

                        // Save PDF page as high-resolution image
                        PdfDrawOptions options = PdfDrawOptions.Create();
                        options.BackgroundColor = new PdfRgbColor(255, 255, 255);
                        options.HorizontalResolution = 600;
                        options.VerticalResolution = 600;

                        string pageImage = $"page_{i}.png";
                        page.Save(pageImage, options);

                        using (Pix img = Pix.LoadFromFile(pageImage))
                        {
                            using (Page recognizedPage = engine.Process(img))
                            {
                                var recognizedText = recognizedPage.GetText();
                                Console.WriteLine($"Mean confidence for page #{i}: {recognizedPage.GetMeanConfidence()}");

                                documentText.Append(recognizedText);
                            }
                        }

                        File.Delete(pageImage);
                    }
                }
            }

            const string Result = "result.txt";
            using (var writer = new StreamWriter(Result))
                writer.Write(documentText.ToString());

            Process.Start(Result);
        }
    }
}