using System.Diagnostics;

using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractTextByWords
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "ExtractTextByWords.pdf";

            using (PdfDocument pdf = new PdfDocument(@"Sample Data\form.pdf"))
            {
                PdfPage page = pdf.Pages[0];
                foreach (PdfTextData data in page.GetWords())
                    page.Canvas.DrawRectangle(data.Bounds);

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
