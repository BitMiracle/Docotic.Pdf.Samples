using System;
using System.Diagnostics;
using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class Tables
    {
        static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            const string PathToFile = "Tables.pdf";
            PdfDocumentBuilder.Create().Generate(PathToFile, doc =>
            {
                doc.Pages(page =>
                {
                    page.TextStyle(t => t.Parent.FontSize(8))
                        .MarginVertical(50)
                        .MarginHorizontal(10);

                    page.Content().Width(400).Table(t =>
                    {
                        t.Columns(c =>
                        {
                            c.RelativeColumn(2);
                            c.RelativeColumn(2);
                            c.RelativeColumn(3);
                        });

                        t.Header(h =>
                        {
                            headerCell(h).Text("Project");
                            headerCell(h).Text("License");
                            headerCell(h).Text("Description");
                        });

                        for (int i = 0; i < Products.Length; ++i)
                        {
                            bool alt = i % 2 == 1;
                            Product p = Products[i];
                            bodyCell(t, alt, p.Licenses.Length).Text(p.Name);
                            foreach (License l in p.Licenses)
                            {
                                bodyCell(t, alt).Text(l.Name);
                                bodyCell(t, alt).Text(l.Description);
                            }
                        }
                    });
                });
            });

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }

        private static LayoutContainer headerCell(TableCellContainer t)
        {
            return t.Cell()
                .Background(new PdfGrayColor(75))
                .Border(b => b.Thickness(CellBorderThickness))
                .PaddingVertical(CellVerticalPadding)
                .AlignCenter()
                .AlignMiddle();
        }

        private static LayoutContainer bodyCell(Table t, bool alt, int rowSpan = 1)
        {
            return t.Cell(c => c.RowSpan(rowSpan))
                .Container(c => alt ? c.Background(new PdfGrayColor(90)) : c)
                .Border(b => b.Thickness(CellBorderThickness))
                .PaddingVertical(CellVerticalPadding)
                .AlignCenter()
                .AlignMiddle();
        }

        private const double CellVerticalPadding = 10;
        private const double CellBorderThickness = 0.1;

        private static readonly Product[] Products = new Product[]
        {
            new("Docotic.Pdf", new License[]
            {
                new("Application License", "Desktop or mobile applications"),
                new("Server License", "Server-based services"),
                new("Ultimate License", "All your PDF needs"),
                new("Utlimate+ License", "All your PDF needs + premium support"),
            }),
            new("Jpeg2000.Net", new License[] { new("Ultimate License", "Any services or applications") }),
            new("LibTiff.Net", new License[] { new("Free", "") }),
            new("LibJpeg.Net", new License[] { new("Free", "") }),
        };
    }

    record Product(string Name, License[] Licenses);
    record License(string Name, string Description);
}