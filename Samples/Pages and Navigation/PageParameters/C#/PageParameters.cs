using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class PageParameters
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "PageParameters.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.InitialView = pdf.CreateView(0);
                pdf.InitialView.SetFitPage();

                PdfPage firstPage = pdf.Pages[0];
                firstPage.Size = PdfPaperSize.A6;

                PdfPage secondPage = pdf.AddPage();
                secondPage.Width = 300;
                secondPage.Height = 100;

                PdfPage thirdPage = pdf.AddPage();
                thirdPage.Rotation = PdfRotation.Rotate90;

                PdfPage fourthPage = pdf.AddPage();
                fourthPage.Orientation = PdfPaperOrientation.Landscape;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
