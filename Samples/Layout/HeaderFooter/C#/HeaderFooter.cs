using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class HeaderFooter
    {
        static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string PathToFile = "HeaderFooter.pdf";
            PdfDocumentBuilder.Create().Generate(PathToFile, doc =>
            {
                doc.Typography(t =>
                {
                    t.Header = t.Emphasis.FontSize(16).Underline();
                    t.Footer = t.Plain.FontSize(8);
                });

                doc.Pages(page =>
                {
                    page.Size(PdfPaperSize.A5).MarginHorizontal(50);

                    page.Header()
                        .Height(60)
                        .AlignRight()
                        .AlignBottom()
                        .Text("Page header");

                    page.Content().PaddingVertical(10).Column(c =>
                    {
                        PdfGrayColor columnBg = new(95);
                        c.Header()
                            .Background(columnBg)
                            .Text(t =>
                            {
                                t.Span("Column header (");
                                t.CurrentPageNumber();
                                t.Span(" / ");
                                t.PageCount();
                                t.Span(")");
                            });

                        c.Item().Table(table =>
                        {
                            table.Columns(c => c.RelativeColumn());

                            PdfGrayColor tableBg = new(70);
                            table.Header(h => h.Cell().Background(tableBg).Text("Table header"));

                            string loremIpsum = File.ReadAllText("../Sample Data/lorem-ipsum.txt");
                            table.Cell().Text(loremIpsum);

                            table.Footer(f => f.Cell().Background(tableBg).Text("Table footer"));
                        });

                        c.Footer()
                            .Background(columnBg)
                            .Text("Column footer");
                    });

                    page.Footer()
                        .Height(30)
                        .AlignCenter()
                        .Text(t => t.CurrentPageNumber().FontColor(new PdfRgbColor(255, 0, 0)).Format(n => n?.ToRoman() ?? string.Empty));
                });
            });

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }

    static class RomanNumberFormatter
    {
        private static readonly int[] Digits = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
        private static readonly string[] RomanDigits = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };

        public static string ToRoman(this int number)
        {
            StringBuilder result = new();
            while (number > 0)
            {
                for (int i = Digits.Length - 1; i >= 0; --i)
                {
                    if (number / Digits[i] >= 1)
                    {
                        number -= Digits[i];
                        result.Append(RomanDigits[i]);
                        break;
                    }
                }
            }
            return result.ToString();
        }
    }
}
