using System;
using System.Diagnostics;
using System.IO;
using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class TypographySample
    {
        static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string PathToFile = "Typography.pdf";
            PdfDocumentBuilder.Create().Generate(PathToFile, doc =>
            {
                doc.Typography(new CustomTypography(doc));

                doc.Pages(page =>
                {
                    page.MarginTop(50);

                    page.Header().Border(b => b.Bottom(1)).Text("Header");

                    page.Content().Text(t =>
                    {
                        t.Line("Regular");

                        t.Line("Title").Style(t => t.Title);
                        t.Line("SubTitle").Style(t => t.SubTitle);
                        t.Line("Heading1").Style(t => t.Heading1);
                        t.Line("Heading2").Style(t => t.Heading2);
                        t.Line("Heading3").Style(t => t.Heading3);

                        t.Line("Caption").Style(t => t.Caption);
                        t.Line("Footnote").Style(t => t.Footnote);

                        t.Hyperlink("Hyperlink", new Uri("https://bitmiracle.com"));
                        t.Line();

                        t.Line("Emphasis").Style(t => t.Emphasis);
                        t.Line("Strong").Style(t => t.Strong);
                        t.Line("printf(\"Plain\");").Style(t => t.Plain);

                        t.Line("Sample").Style(t => ((CustomTypography)t).Sample);
                    });

                    page.Footer().Border(b => b.Top(1)).Text(t =>
                    {
                        t.Span("Footer ");
                        t.CurrentPageNumber();
                    });
                });
            });

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }

    class CustomTypography : Typography
    {
        public CustomTypography(Document doc)
            : base(doc)
        {
            // Use custom font in the default document style
            FileInfo roboto = new(@"..\Sample Data\Fonts\Roboto\Roboto-Regular.ttf");
            Document = doc.TextStyleWithFont(roboto);

            // Customize size of the page header font
            Header = Parent.FontSize(8);

            // Customize size and style of the page footer font
            Footer = Emphasis.FontSize(8);

            // Custom style
            Sample = Parent.Strikethrough().Underline().FontColor(new PdfRgbColor(255, 0, 0));
        }

        public TextStyle Sample { get; }
    }
}
