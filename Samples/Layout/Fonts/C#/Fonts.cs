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
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            const string PathToFile = "Fonts.pdf";
            PdfDocumentBuilder
                .Create()
                .FontLoader(new DirectoryFontLoader(new[] { @"..\Sample Data\Fonts" }, true))
                .MissingGlyphHandler(_ => "?")
                .Generate(PathToFile, doc =>
                {
                    // Load font using a current font loader
                    TextStyle roboto = doc.TextStyleWithFont(SystemFont.Family("Roboto").Bold());

                    // Or provide file path / stream explicitly
                    FileInfo path = new(@"..\Sample Data\Fonts\Algerian.ttf");
                    TextStyle holiday = doc.TextStyleWithFont(path, FontEmbedMode.EmbedUsedGlyphs);

                    doc.Pages(page =>
                    {
                        page.TextStyle(holiday.Fallback(roboto));

                        page.MarginTop(50);

                        page.Content().Text(t =>
                        {
                            t.Line("Regular text");

                            t.Line("Fallback: ₩Ѻ");

                            t.Line("Missing glyph handler: 💁👌🎍😍");
                        });
                    });
                });

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}
