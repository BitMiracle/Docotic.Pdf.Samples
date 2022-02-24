using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class DrawText
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "DrawText.pdf";

            using (PdfDocument pdf = new PdfDocument())
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
        }
    }
}