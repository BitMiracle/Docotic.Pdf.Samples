using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class CreateNamesDocument
    {
        private const double LeftMargin = 80; // the width of the left margin of the document
        private const double TopMargin = 50; // the height of the top margin of the document
        private const double RightMargin = 50; // the width of the right margin of the document

        private const double FontSize = 10; // font size in the document
        private const double LoremIpsumFontSize = 7; // font size of the lorem ipsum part

        public static void Run()
        {
            string pathToFile = "CreateNamesDocument.pdf";
            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                PdfCanvas canvas = page.Canvas;
                canvas.FontSize = FontSize;

                DrawNameField(canvas, 40, page, 0);
                DrawLoremIpsum(canvas, page.Width);
                DrawNameField(canvas, 200, page, 1);

                pdf.Save(pathToFile);
            }

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void DrawNameField(
            PdfCanvas canvas, double offsetFromTopMargin, PdfPage page, int nameFieldIndex)
        {
            const string caption = "First name:";

            double left = LeftMargin;
            double top = TopMargin + offsetFromTopMargin;
            canvas.DrawString(left, top, caption);

            PdfSize captionSize = canvas.MeasureText(caption);
            double width = page.Width - RightMargin - LeftMargin - captionSize.Width;

            PdfTextBox textBox = page.AddTextBox(
                "name" + nameFieldIndex,
                canvas.TextPosition.X,
                canvas.TextPosition.Y,
                width,
                captionSize.Height);
            textBox.Font = canvas.Font;
            textBox.FontSize = FontSize;
            textBox.Border.Width = 0;
            textBox.TextAlign = PdfTextAlign.Center;
        }

        private static void DrawLoremIpsum(PdfCanvas canvas, double pageWidth)
        {
            double fontSizeBefore = canvas.FontSize;
            try
            {
                var loremIpsumBounds = new PdfRectangle(
                    LeftMargin,
                    TopMargin + 60,
                    pageWidth - RightMargin - LeftMargin,
                    150
                );
                canvas.FontSize = LoremIpsumFontSize;

                var loremIpsum = string.Join(
                    Environment.NewLine,
                    File.ReadAllLines("../Sample Data/lorem-ipsum.txt").Take(5));
                var options = new PdfTextDrawingOptions(loremIpsumBounds)
                {
                    HorizontalAlignment = PdfTextAlign.Left,
                    VerticalAlignment = PdfVerticalAlign.Top
                };
                canvas.DrawText(loremIpsum, options);
            }
            finally
            {
                canvas.FontSize = fontSizeBefore;
            }
        }
    }
}
