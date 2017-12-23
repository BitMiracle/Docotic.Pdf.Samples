using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Watermarks
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "Watermarks.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.AddPage();

                PdfXObject watermark = pdf.CreateXObject();

                // uncomment following line if you want the watermark to be placed before
                // any other contents of a page.
                // watermark.DrawOnBackground = true;

                PdfCanvas watermarkCanvas = watermark.Canvas;
                watermarkCanvas.TextAngle = -45;
                watermarkCanvas.FontSize = 36;
                watermarkCanvas.DrawString(100, 100, "This text is a part of the watermark");

                foreach (PdfPage page in pdf.Pages)
                {
                    // draw watermark at the top left corner over the page contents
                    // this will also add watermark to the collection of page XObjects
                    page.Canvas.DrawXObject(watermark, 0, 0);
                }

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}