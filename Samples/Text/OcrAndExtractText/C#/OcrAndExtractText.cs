using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

using Tesseract;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class OcrAndExtractText
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var documentText = new StringBuilder();
            using (var pdf = new PdfDocument(@"..\Sample data\Freedman Scora.pdf"))
            {
                var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (location == null)
                {
                    Console.WriteLine("Invalid assembly location");
                    return;
                }

                var tessData = Path.Combine(location, @"tessdata");
                using var engine = new TesseractEngine(tessData, "eng", EngineMode.LstmOnly);

                for (int i = 0; i < pdf.PageCount; ++i)
                {
                    if (documentText.Length > 0)
                        documentText.Append("\r\n\r\n");

                    PdfPage page = pdf.Pages[i];
                    string searchableText = page.GetText();

                    // Simple check if the page contains searchable text.
                    // We do not need to perform OCR in that case.
                    if (!string.IsNullOrEmpty(searchableText.Trim()))
                    {
                        documentText.Append(searchableText);
                        continue;
                    }

                    // This page is not searchable.
                    // Save the PDF page as a high-resolution image.
                    PdfDrawOptions options = PdfDrawOptions.Create();
                    options.BackgroundColor = new PdfRgbColor(255, 255, 255);
                    options.HorizontalResolution = 200;
                    options.VerticalResolution = 200;

                    string pageImage = $"page_{i}.png";
                    page.Save(pageImage, options);

                    // Perform OCR
                    using (Pix img = Pix.LoadFromFile(pageImage))
                    {
                        using Page recognizedPage = engine.Process(img);
                        Console.WriteLine($"Mean confidence for page #{i}: {recognizedPage.GetMeanConfidence()}");

                        string recognizedText = recognizedPage.GetText();
                        documentText.Append(recognizedText);
                    }

                    File.Delete(pageImage);
                }
            }

            const string Result = "result.txt";
            using (var writer = new StreamWriter(Result))
                writer.Write(documentText.ToString());

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(Result) { UseShellExecute = true });
        }
    }
}