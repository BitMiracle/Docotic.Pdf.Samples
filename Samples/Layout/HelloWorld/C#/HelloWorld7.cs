using System;
using System.Globalization;
using System.IO;

using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class HelloWorld7
    {
        public static void CreatePdf()
        {
            PdfDocumentBuilder.Create().Generate("hello.pdf", BuildDocument);
        }

        static void BuildDocument(Document doc)
        {
            doc.Typography(t =>
            {
                t.Document = doc.TextStyleWithFont(SystemFont.Family("Calibri"));
            });

            var imageFile = new FileInfo(@"..\Sample Data\red-flowers-at-butterfly-world.jpg");
            var image = doc.Image(imageFile);

            doc.Pages(pages => {
                BuildPages(pages);

                pages.Content().Column(c =>
                {
                    BuildTextContent(c.Item());
                    BuildImageContent(c.Item(), image);
                    BuildListContent(c.Item());
                    BuildTableContent(c.Item());
                });
            });
        }

        static void BuildTableContent(LayoutContainer content)
        {
            var color = new PdfGrayColor(75);

            content.PaddingTop(20).Table(t =>
            {
                t.Columns(c =>
                {
                    c.RelativeColumn(4);
                    c.RelativeColumn(1);
                    c.RelativeColumn(4);
                });

                t.Header(h =>
                {
                    h.Cell().Background(color).Text("Month in 2024");
                    h.Cell().Background(color).Text("Days");
                    h.Cell().Background(color).Text("First Day");
                });

                for (int i = 0; i < 12; i++)
                {
                    var stats = GetMonthStats(2024, i);

                    t.Cell().Text(stats.Item1);
                    t.Cell().Text(stats.Item2);
                    t.Cell().Text(stats.Item3);
                }
            });
        }

        static (string, string, string) GetMonthStats(int year, int monthIndex)
        {
            return (
                DateTimeFormatInfo.InvariantInfo.MonthNames[monthIndex],
                DateTime.DaysInMonth(year, monthIndex + 1).ToString(),
                new DateTime(year, monthIndex + 1, 1).DayOfWeek.ToString()
            );
        }

        static void BuildListContent(LayoutContainer content)
        {
            var dayNames = DateTimeFormatInfo.InvariantInfo.DayNames;
            var dayNamesSpain = DateTimeFormatInfo.GetInstance(new CultureInfo("es-ES")).DayNames;

            content.Column(column =>
            {
                for (int i = 0; i < dayNames.Length; i++)
                {
                    column.Item().Row(row =>
                    {
                        row.Spacing(5);
                        row.AutoItem().Text($"{i + 1}.");
                        row.RelativeItem().Text(t => {
                            t.Line(dayNames[i]);
                            t.Line($"In Spain they call it {dayNamesSpain[i]}");
                        });
                    });
                }
            });
        }

        static void BuildImageContent(LayoutContainer content, Image image)
        {
            content.AlignCenter()
                .PaddingVertical(20)
                .Image(image);
        }

        static void BuildPages(PageLayout pages)
        {
            pages.Size(PdfPaperSize.A6);
            pages.Margin(10);

            BuildPagesHeader(pages.Header());
            BuildPagesFooter(pages.Footer());
        }

        static void BuildTextContent(LayoutContainer content)
        {
            var colorAccented = new PdfRgbColor(56, 194, 10);
            var styleAccented = TextStyle.Parent.FontColor(colorAccented);

            content.Text(t =>
            {
                t.Span("Hello, ").Style(styleAccented);
                t.Span("world!").Style(styleAccented.Underline());
            });
        }

        static void BuildPagesHeader(LayoutContainer header)
        {
            header.TextStyle(TextStyle.Parent.FontSize(8))
                .AlignRight()
                .Text(t =>
                {
                    t.Line($"Created by: {Environment.UserName}");
                    t.Line($"Date: {DateTime.Now}");
                });
        }

        static void BuildPagesFooter(LayoutContainer footer)
        {
            footer.Height(20)
                .Background(new PdfGrayColor(95))
                .AlignCenter()
                .Text(t => t.CurrentPageNumber());
        }
    }
}
