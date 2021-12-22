using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CreateAndSaveDocument
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "CreateAndSaveDocument.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                // let's draw "Hello, world" on the first page
                PdfPage firstPage = pdf.Pages[0];
                firstPage.Canvas.DrawString(10, 50, "Hello, world!");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}