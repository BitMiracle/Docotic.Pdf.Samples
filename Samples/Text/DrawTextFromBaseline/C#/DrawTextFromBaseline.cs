using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class DrawTextFromBaseline
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "DrawTextFromBaseline.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.FontSize = 30;
                canvas.Font = pdf.AddFont(PdfBuiltInFont.TimesItalic);

                // draw a straight line across the page
                const double baselinePosition = 100;
                canvas.CurrentPosition = new PdfPoint(0, baselinePosition);
                canvas.DrawLineTo(new PdfPoint(pdf.Pages[0].Width, baselinePosition));

                // calculate the distance from the baseline to the top of the font bounding box
                double distanceToBaseLine = GetDistanceToBaseline(canvas.Font, canvas.FontSize);

                // draw text so that the baseline of the text is exactly over the straight line
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
