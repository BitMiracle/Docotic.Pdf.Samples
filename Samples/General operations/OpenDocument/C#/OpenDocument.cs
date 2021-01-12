using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class OpenDocument
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "OpenDocument.pdf";

            using (PdfDocument pdf = new PdfDocument("Sample data/jfif3.pdf"))
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.Font = pdf.AddFont(PdfBuiltInFont.Helvetica);
                canvas.FontSize = 20;
                canvas.DrawString(10, 80, "This text was added by Docotic.Pdf");

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}