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
                // A new PDF document already contains one page
                PdfPage firstPage = pdf.Pages[0];
                firstPage.Canvas.DrawString("This is the auto-created first page");

                // Add a new page at the end of the document
                PdfPage secondPage = pdf.Pages.Add();
                secondPage.Canvas.DrawString("Second page");

                // Insert a new page at the specified position
                PdfPage newFirstPage = pdf.Pages.Insert(0);
                newFirstPage.Canvas.DrawString("New first page");

                // Inserting at Count is equivalent to calling Add
                PdfPage lastPage = pdf.Pages.Insert(pdf.Pages.Count);
                lastPage.Canvas.DrawString("Last page");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
