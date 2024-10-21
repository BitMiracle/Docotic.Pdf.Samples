using System;
using System.Diagnostics;
using System.IO;
using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class Fonts
    {
        static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string PathToFile = "Fonts.pdf";
            PdfDocumentBuilder
                .Create()
                .FontLoader(new DirectoryFontLoader(new[] { @"..\Sample Data\Fonts" }, true))
                .MissingGlyphHandler(_ => "?")
                .Generate(PathToFile, doc =>
                {
                    // Load the font using the current font loader
                    TextStyle roboto = doc.TextStyleWithFont(SystemFont.Family("Roboto").Bold());

                    // Or provide the file path / stream explicitly
                    FileInfo path = new(@"..\Sample Data\Fonts\Algerian.ttf");
                    TextStyle holiday = doc.TextStyleWithFont(path);

                    TextStyle hebrew = doc.TextStyleWithFont(SystemFont.Family("Noto Sans Hebrew"), FontEmbedMode.EmbedAllGlyphs);

                    doc.Pages(page =>
                    {
                        page.TextStyle(holiday.Fallback(roboto))
                            .MarginTop(50)
                            .Content()
                            .Text(t =>
                            {
                                t.Line("Regular text");

                                t.Line("Fallback: ₩Ѻ");

                                t.Line("Missing glyph handler: 💁👌🎍😍");

                                t.Line("RTL text: השעה").Style(hebrew);
                            });
                    });
                });

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}
