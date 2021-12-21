using System;
using System.IO;
using System.Reflection;

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

            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using (PdfDocument pdf = new PdfDocument(Path.Combine(location, "jfif3.pdf")))
            {
                pdf.PageLayout = PdfPageLayout.TwoColumnLeft;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}