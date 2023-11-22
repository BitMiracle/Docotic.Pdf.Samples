using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ErrorHandling
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "ErrorHandling.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                try
                {
                    // tries to draw Russian string with built-in Helvetica font which doesn't
                    // define glyphs for Russian characters. So, we expect that a PdfException will be thrown.
                    page.Canvas.DrawString(10, 50, "Привет, мир!");
                }
                catch (CannotShowTextException ex)
                {
                    PdfFont? arial = pdf.AddFont("Arial");
                    if (arial is null)
                    {
                        Console.WriteLine("Cannot use Arial font");
                        return;
                    }

                    page.Canvas.Font = arial;
                    page.Canvas.DrawString(10, 50, "Expected exception occurred:");
                    page.Canvas.DrawString(10, 65, ex.Message);
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}