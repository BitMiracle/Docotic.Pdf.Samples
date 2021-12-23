using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class LinkToPage
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "LinkToPage.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.AddPage();
                pdf.AddPage();

                PdfPage firstPage = pdf.Pages[0];
                PdfCanvas canvas = firstPage.Canvas;
                PdfRectangle rectForLinkToSecondPage = new PdfRectangle(10, 50, 100, 60);
                canvas.DrawRectangle(rectForLinkToSecondPage, PdfDrawMode.Stroke);
                canvas.DrawString("Go to 2nd page", rectForLinkToSecondPage, PdfTextAlign.Center, PdfVerticalAlign.Center);
                firstPage.AddLinkToPage(rectForLinkToSecondPage, 1);

                PdfRectangle rectForLinkToThirdPage = new PdfRectangle(150, 50, 100, 60);
                canvas.DrawRectangle(rectForLinkToThirdPage, PdfDrawMode.Stroke);
                canvas.DrawString("Go to 3rd page", rectForLinkToThirdPage, PdfTextAlign.Center, PdfVerticalAlign.Center);
                firstPage.AddLinkToPage(rectForLinkToThirdPage, pdf.Pages[2]);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
