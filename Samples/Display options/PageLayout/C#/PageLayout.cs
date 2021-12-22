using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class PageLayout
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "PageLayout.pdf";

            using (PdfDocument pdf = new PdfDocument(@"..\Sample Data\jfif3.pdf"))
            {
                pdf.PageLayout = PdfPageLayout.TwoColumnLeft;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}