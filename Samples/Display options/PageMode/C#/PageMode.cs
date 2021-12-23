using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class PageMode
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "PageMode.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.PageMode = PdfPageMode.FullScreen;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}