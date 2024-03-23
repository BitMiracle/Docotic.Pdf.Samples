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
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

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
