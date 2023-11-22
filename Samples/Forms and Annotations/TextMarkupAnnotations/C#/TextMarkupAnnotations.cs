using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextMarkupAnnotations
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string pathToFile = "TextMarkupAnnotations.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];
                PdfCanvas canvas = page.Canvas;

                // Draw text on the page
                const string Text = "Highlighted text";
                var textPosition = new PdfPoint(10, 50);
                canvas.FontSize = 30;
                canvas.DrawString(textPosition, Text);

                // Get size of the drawn text
                PdfSize size = canvas.MeasureText(Text);
                var bounds = new PdfRectangle(textPosition, size);

                // Highlight and annotate the text
                var color = new PdfRgbColor(0, 0, 255);
                const string AnnotationContents = "Lorem ipsum";
                page.AddHighlightAnnotation(AnnotationContents, bounds, color);

                // Or call these method to strike out or underline the text:
                // page.AddStrikeoutAnnotation(AnnotationContents, bounds, color);
                // page.AddJaggedUnderlineAnnotation(AnnotationContents, bounds, color);
                // page.AddUnderlineAnnotation(AnnotationContents, bounds, color);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
