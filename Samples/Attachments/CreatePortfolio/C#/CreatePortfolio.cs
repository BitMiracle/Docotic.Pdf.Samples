using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CreatePortfolio
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "CreatePortfolio.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfFileSpecification first = pdf.CreateFileAttachment(@"..\Sample Data\jpeg.pdf");
                pdf.SharedAttachments.Add(first);

                PdfFileSpecification second = pdf.CreateFileAttachment(@"..\Sample Data\gmail-cheat-sheet.pdf");
                pdf.SharedAttachments.Add(second);

                PdfCollectionSettings col = pdf.CreateCollectionSettings();
                pdf.Collection = col;
                col.InitialDocument = "jpeg.pdf";
                col.SetTiledView();

                pdf.Pages[0].Canvas.DrawString("Your PDF viewer does not support portfolios");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
