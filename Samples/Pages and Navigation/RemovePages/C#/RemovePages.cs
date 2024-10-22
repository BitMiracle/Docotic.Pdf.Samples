using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemovePages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "RemovePages.pdf";
            using (var pdf = new PdfDocument())
            {
                PdfPage firstPage = pdf.Pages[0];
                firstPage.Canvas.DrawString(10, 50, "Main page");

                // add a page to the end and then delete it
                pdf.AddPage();
                pdf.RemovePage(pdf.PageCount - 1);

                // another way to delete a page
                PdfPage pageToDelete = pdf.AddPage();
                pdf.RemovePage(pageToDelete);

                // we can delete several pages at once
                pdf.InsertPage(0);
                pdf.InsertPage(1);
                pdf.RemovePages(new int[] { 0, 1 });

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
