using System;
using System.Diagnostics;
using System.IO;
using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class Pages
    {
        static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string PathToFile = "Pages.pdf";
            PdfDocumentBuilder.Create().Generate(PathToFile, doc =>
            {
                doc.Pages(page =>
                {
                    page.Size(PdfPaperSize.A4, PdfPaperOrientation.Portrait)
                        .MarginVertical(50)
                        .MarginHorizontal(40)
                        .BackgroundColor(new PdfGrayColor(95));

                    PdfRgbColor watermarkColor = new(255, 0, 0);
                    page.Foreground()
                        .Extend()
                        .AlignCenter()
                        .AlignMiddle()
                        .TranslateX(50)
                        .TranslateY(200)
                        .Rotate(-55)
                        .Text("Watermark")
                        .Style(t => t.Parent.FontSize(100).FontColor(watermarkColor, 50));

                    page.Header()
                        .PaddingBottom(30)
                        .AlignRight()
                        .Text("Header");

                    string loremIpsum = File.ReadAllText(@"..\Sample Data\lorem-ipsum.txt");
                    page.Content().Text(loremIpsum);

                    page.Footer()
                        .AlignCenter()
                        .Text(t => t.CurrentPageNumber());
                });

                doc.Pages(page =>
                {
                    page.Size(PdfPaperSize.A3, PdfPaperOrientation.Landscape)
                        .MarginVertical(50)
                        .ContentFromRightToLeft()
                        .Content()
                        .Text("Hello World");
                });

                doc.Pages(page =>
                {
                    page.Size(100, 100)
                        .Content()
                        .Text("Custom page size");
                });
            });

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}
