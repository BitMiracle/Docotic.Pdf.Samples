using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class DrawText
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "DrawText.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.DrawString(10, 50, "Hello, world!");

                const string LongString = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
                var singleLineOptions = new PdfTextDrawingOptions(new PdfRectangle(10, 70, 40, 150))
                {
                    Multiline = false,
                    HorizontalAlignment = PdfTextAlign.Left,
                    VerticalAlignment = PdfVerticalAlign.Top
                };
                canvas.DrawText(LongString, singleLineOptions);

                var multiLineOptions = new PdfTextDrawingOptions(new PdfRectangle(70, 70, 40, 150))
                {
                    HorizontalAlignment = PdfTextAlign.Left,
                    VerticalAlignment = PdfVerticalAlign.Top
                };
                canvas.DrawText(LongString, multiLineOptions);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}