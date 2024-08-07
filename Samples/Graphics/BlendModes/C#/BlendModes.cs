﻿using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class BlendModes
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "BlendModes.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                var location = new PdfPoint(50, 80);
                var size = new PdfSize(50, 50);

                var modes = new PdfBlendMode[] { PdfBlendMode.Hue, PdfBlendMode.Lighten, PdfBlendMode.Darken };
                for (int i = 0; i < modes.Length; ++i)
                {
                    if (i != 0)
                        location.X += size.Width * 2;

                    canvas.BlendMode = PdfBlendMode.Normal;

                    canvas.Brush.Color = new PdfRgbColor(0, 0, 0);
                    canvas.DrawString(location.X, location.Y - 20, "BlendMode: " + modes[i].ToString());

                    canvas.Brush.Color = new PdfRgbColor(200, 140, 7);
                    canvas.DrawRectangle(new PdfRectangle(location, size), 0, PdfDrawMode.Fill);

                    canvas.BlendMode = modes[i];
                    canvas.Brush.Color = new PdfRgbColor(125, 125, 255);
                    var secondRect = new PdfRectangle(location.X + 15, location.Y + 15, size.Width, size.Height);
                    canvas.DrawRectangle(secondRect, 0, PdfDrawMode.Fill);
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}