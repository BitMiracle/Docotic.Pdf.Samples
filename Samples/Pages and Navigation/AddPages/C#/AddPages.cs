using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddPages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string pathToFile = "AddPages.pdf";

            using (var pdf = new PdfDocument())
            {
                // There is already one page in PDF document after creation
                PdfPage firstPage = pdf.Pages[0];
                firstPage.Canvas.DrawString("This is the auto-created first page");

                // AddPage method adds a new page to the end of the PDF document
                PdfPage secondPage = pdf.AddPage();
                secondPage.Canvas.DrawString("Second page");

                // You can insert a new page in an arbitrary place
                PdfPage newFirstPage = pdf.InsertPage(0);
                newFirstPage.Canvas.DrawString("New first page");

                // This call is an equivalent of AddPage method
                PdfPage lastPage = pdf.InsertPage(pdf.PageCount);
                lastPage.Canvas.DrawString("Last page");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
