using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class DrawTextFromBaseline
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string pathToFile = "DrawTextFromBaseline.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.FontSize = 30;
                canvas.Font = pdf.AddFont(PdfBuiltInFont.TimesItalic);

                // draw straight line across the page
                const double baselinePosition = 100;
                canvas.CurrentPosition = new PdfPoint(0, baselinePosition);
                canvas.DrawLineTo(new PdfPoint(pdf.Pages[0].Width, baselinePosition));

                // calculate distance from the baseline to the top of the font bounding box
                double distanceToBaseLine = GetDistanceToBaseline(canvas.Font, canvas.FontSize);

                // draw text so that baseline of the text placed exactly over the straight line
                canvas.DrawString(10, baselinePosition - distanceToBaseLine, "gyQW");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static double GetDistanceToBaseline(PdfFont font, double fontSize)
        {
            return font.TopSideBearing * font.TransformationMatrix.M22 * fontSize;
        }
    }
}
