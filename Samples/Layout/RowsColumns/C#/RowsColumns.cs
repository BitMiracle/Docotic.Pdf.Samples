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

            const string PathToFile = "RowsColumns.pdf";
            PdfDocumentBuilder.Create().Generate(PathToFile, doc =>
            {
                doc.Pages(page =>
                {
                    page.Margin(50);

                    page.Content().Column(c =>
                    {
                        c.Item().TextStyle(t => t.Parent.FontSize(8)).Row(r =>
                        {
                            static void border(Border b) => b.Thickness(0.001);
                            r.ConstantItem(200).Border(border).Padding(5).Text(t =>
                            {
                                t.Line("First name: John");
                                t.Line("Last name: Doe");
                                t.Line("Age: 50");
                            });

                            r.AutoItem().Border(border).Padding(5).RotateRight().Text("Ammerland");

                            r.RelativeItem(1).Border(border).Padding(5).Column(c =>
                            {
                                c.Item().AlignCenter().Text("Photo");

                                FileInfo imagePath = new(@"..\Sample Data\ammerland.jpg");
                                Image image = doc.Image(imagePath);
                                c.Item().AlignCenter().MaxWidth(100).Image(image);
                            });
                        });

                        string loremIpsum = File.ReadAllText(@"..\Sample Data\lorem-ipsum.txt");
                        c.Item().PaddingTop(10).Text(loremIpsum);
                    });
                });
            });

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}
