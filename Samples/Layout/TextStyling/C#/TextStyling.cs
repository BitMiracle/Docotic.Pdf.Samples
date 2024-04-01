using System;
using System.Diagnostics;
using System.IO;
using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class TextStyling
    {
        static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            const string PathToFile = "TextStyling.pdf";
            PdfDocumentBuilder.Create().Generate(PathToFile, doc =>
            {
                TextStyle root = TextStyle.Parent.FontColor(new PdfRgbColor(255, 0, 0)).FontSize(30);

                TextStyle child = TextStyle.Parent
                    .FontColor(new PdfRgbColor(0, 0, 255))
                    .Strikethrough()
                    .LetterSpacing(0.5);

                FileInfo holidayFont = new("../Sample Data/Fonts/HolidayPi_BT.ttf");
                TextStyle custom = doc.TextStyleWithFont(holidayFont)
                    .FontSize(20)
                    .FontColor(new PdfRgbColor(255, 165, 0))
                    .BackgroundColor(new PdfGrayColor(90))
                    .LetterSpacing(3)
                    .LineHeight(2);

                doc.Pages(page =>
                {
                    page.TextStyle(root).MarginTop(50);

                    page.Header().Text(t =>
                    {
                        t.Span("Root style");
                        t.Span("superscript").Style(TextStyle.Parent.Superscript());
                    });

                    page.Content().Text(t =>
                    {
                        t.Style(child);

                        t.Line("Child style");

                        t.Line("Custom style").Style(custom.Strikethrough(false));
                    });
                });
            });

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}
