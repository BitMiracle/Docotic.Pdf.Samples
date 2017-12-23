using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextHorizontalScaling
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "TextHorizontalScaling.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                const string sampleText = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.TextHorizontalScaling = 10;
                canvas.DrawString(10, 50, sampleText);

                canvas.TextHorizontalScaling = 50;
                canvas.DrawString(10, 60, sampleText);

                canvas.TextHorizontalScaling = 100;
                canvas.DrawString(10, 70, sampleText);

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}