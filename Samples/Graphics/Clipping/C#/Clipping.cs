﻿using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Clipping
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Clipping.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.Pen.Width = 2;
                canvas.Brush.Color = new PdfRgbColor(255, 0, 0);

                DrawStar(canvas);

                canvas.SetClip(PdfFillMode.Alternate);
                canvas.StrokePath();

                canvas.DrawRectangle(new PdfRectangle(0, 0, 500, 500), PdfDrawMode.Fill);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void DrawStar(PdfCanvas canvas)
        {
            canvas.CurrentPosition = new PdfPoint(10, 100);
            canvas.AppendLineTo(110, 100);
            canvas.AppendLineTo(30, 160);
            canvas.AppendLineTo(60, 70);
            canvas.AppendLineTo(80, 160);
            canvas.ClosePath();
        }
    }
}