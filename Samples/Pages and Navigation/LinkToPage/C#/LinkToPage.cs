﻿using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class LinkToPage
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string pathToFile = "LinkToPage.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.AddPage();
                pdf.AddPage();

                PdfPage firstPage = pdf.Pages[0];
                PdfCanvas canvas = firstPage.Canvas;
                var rectForLinkToSecondPage = new PdfRectangle(10, 50, 100, 60);
                canvas.DrawRectangle(rectForLinkToSecondPage, PdfDrawMode.Stroke);
                DrawCenteredText(canvas, "Go to 2nd page", rectForLinkToSecondPage);
                firstPage.AddLinkToPage(rectForLinkToSecondPage, 1);

                var rectForLinkToThirdPage = new PdfRectangle(150, 50, 100, 60);
                canvas.DrawRectangle(rectForLinkToThirdPage, PdfDrawMode.Stroke);
                DrawCenteredText(canvas, "Go to 3rd page", rectForLinkToThirdPage);
                firstPage.AddLinkToPage(rectForLinkToThirdPage, pdf.Pages[2]);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void DrawCenteredText(PdfCanvas canvas, string text, PdfRectangle bounds)
        {
            var options = new PdfTextDrawingOptions(bounds)
            {
                HorizontalAlignment = PdfTextAlign.Center,
                VerticalAlignment = PdfVerticalAlign.Center
            };
            canvas.DrawText(text, options);
        }
    }
}
