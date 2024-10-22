using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddPages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "AddPages.pdf";

            using (var pdf = new PdfDocument())
            {
                // There is already one page in any new PDF document
                PdfPage firstPage = pdf.Pages[0];
                firstPage.Canvas.DrawString("This is the auto-created first page");

                // The AddPage method adds a new page to the end of the PDF document
                PdfPage secondPage = pdf.AddPage();
                secondPage.Canvas.DrawString("Second page");

                // You can insert a new page in an arbitrary place
                PdfPage newFirstPage = pdf.InsertPage(0);
                newFirstPage.Canvas.DrawString("New first page");

                // This call is an equivalent to calling the AddPage method
                PdfPage lastPage = pdf.InsertPage(pdf.PageCount);
                lastPage.Canvas.DrawString("Last page");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
