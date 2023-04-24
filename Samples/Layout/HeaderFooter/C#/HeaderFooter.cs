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
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

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
                    page.Size(PdfPaperSize.A5);
                    page.MarginHorizontal(50);

                    page.Header()
                        .Height(60)
                        .AlignRight()
                        .AlignBottom()
                        .Text("Page header");

                    string loremIpsum = File.ReadAllText("../Sample Data/lorem-ipsum.txt");
                    page.Content().PaddingVertical(10).Text(loremIpsum);

                    page.Footer()
                        .Height(30)
                        .AlignCenter()
                        .Text(t => t.CurrentPageNumber().Format(n => n?.ToRoman() ?? string.Empty));
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
