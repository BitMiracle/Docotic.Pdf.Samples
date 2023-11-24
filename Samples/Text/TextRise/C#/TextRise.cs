using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextRise
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "TextRise.pdf";

            using (var pdf = new PdfDocument())
            {
                const string sampleText = "Hello!";
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.TextPosition = new PdfPoint(10, 50);

                canvas.TextRise = 0;
                canvas.DrawString(sampleText);
                canvas.TextRise = 5;
                canvas.DrawString(sampleText);
                canvas.TextRise = -10;
                canvas.DrawString(sampleText);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}