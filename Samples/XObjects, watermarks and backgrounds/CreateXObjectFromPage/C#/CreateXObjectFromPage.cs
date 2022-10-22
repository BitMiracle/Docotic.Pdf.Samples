using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CreateXObjectFromPage
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string pathToFile = "CreateXObjectFromPage.pdf";

            using (PdfDocument other = new PdfDocument(@"..\Sample Data\jfif3.pdf"))
            {
                using (PdfDocument pdf = new PdfDocument())
                {
                    PdfXObject firstXObject = pdf.CreateXObject(other.Pages[0]);
                    PdfXObject secondXObject = pdf.CreateXObject(other.Pages[1]);

                    PdfPage page = pdf.Pages[0];
                    double halfOfPage = page.Width / 2;
                    pdf.Pages[0].Canvas.DrawXObject(firstXObject, 0, 0, halfOfPage, 400, 0);
                    pdf.Pages[0].Canvas.DrawXObject(secondXObject, halfOfPage, 0, halfOfPage, 400, 0);

                    pdf.Save(pathToFile);
                }
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
