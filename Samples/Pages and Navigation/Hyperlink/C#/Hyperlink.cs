﻿using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Hyperlink
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Hyperlink.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.AutoCreateUriActions = true;

                PdfPage page = pdf.Pages[0];
                page.Canvas.DrawString(10, 50, "Url: http://bitmiracle.com");

                var rectWithLink = new PdfRectangle(10, 70, 200, 100);
                page.Canvas.DrawRectangle(rectWithLink, PdfDrawMode.Stroke);

                var options = new PdfTextDrawingOptions(rectWithLink)
                {
                    HorizontalAlignment = PdfTextAlign.Center,
                    VerticalAlignment = PdfVerticalAlign.Center
                };
                page.Canvas.DrawText("Go to Google", options);

                page.AddHyperlink(rectWithLink, new Uri("http://google.com"));

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
