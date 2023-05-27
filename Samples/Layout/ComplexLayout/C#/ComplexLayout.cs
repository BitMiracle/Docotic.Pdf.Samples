using System;
using System.Diagnostics;
using System.IO;

using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class ComplexLayout
    {
        static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            const string PathToFile = "ComplexLayout.pdf";
            PdfDocumentBuilder
                .Create()
                .Info(info =>
                {
                    info.Title = "Complex layout example";
                    info.Creator = "Docotic.Pdf samples";
                    info.Author = TestData.CompanyName;
                    info.CreationDate = TestData.CreatedOn;
                })
                .Generate(PathToFile, generate);

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }

        private static void generate(Document doc)
        {
            doc.Typography(t =>
            {
                FileInfo roboto = new(@"..\Sample Data\Fonts\Roboto\Roboto-Regular.ttf");
                t.Document = doc.TextStyleWithFont(roboto);
                t.Header = t.Parent.FontSize(8);

                FileInfo robotoBold = new(@"..\Sample Data\Fonts\Roboto\Roboto-Bold.ttf");
                t.Strong = doc.TextStyleWithFont(robotoBold);
            });

            Image logo = doc.Image(new FileInfo(@"..\Sample Data\logo.png"));

            doc.Pages(page =>
            {
                page.MarginVertical(20).MarginHorizontal(40);

                header(page.Header(), logo);

                content(page.Content());

                footer(page.Footer());
            });
        }

        private static void header(LayoutContainer container, Image logo)
        {
            container
                .Column(c =>
                {
                    c.Item().Row(r =>
                    {
                        r.AutoItem().Height(50).Width(50).Image(logo);
                        r.RelativeItem(1).AlignRight().Text($"Created: {TestData.CreatedOn:yyyy/MM/dd HH:mm:ss}");
                    });

                    c.Item().Border(b => b.Bottom(1)).PaddingBottom(5).Row(r =>
                    {
                        r.RelativeItem(4).Text(TestData.CompanyName);
                        r.RelativeItem(4).AlignCenter().Text($"Phone: {TestData.Phone}");
                        r.RelativeItem(4).AlignRight().Text($"Email: {TestData.Email}");
                    });

                    c.Item().PaddingTop(5).Row(r =>
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            r.RelativeItem(6)
                                .PaddingRight(8)
                                .Layers(l =>
                                {
                                    l.Layer()
                                        .TranslateX(5)
                                        .TranslateY(5)
                                        .Background(new PdfGrayColor(50));

                                    l.PrimaryLayer()
                                        .Border(b => b.Thickness(1))
                                        .Background(new PdfGrayColor(100))
                                        .Padding(5)
                                        .Text(t =>
                                        {
                                            if (i == 0)
                                            {
                                                PatientInfo patient = TestData.Patient;
                                                t.Line($"Patient name: {patient.Name}");
                                                t.Line($"Date of birth: {patient.BirthDate:yyyy/MM/dd}");
                                                t.Line($"Gender: {patient.Gender}");
                                            }
                                            else
                                            {
                                                ClinicInfo clinic = TestData.Clinic;
                                                t.Line($"Clinic: {clinic.Name}");
                                                t.Line($"Address: {clinic.Address}");
                                            }
                                        });
                                });
                        }
                    });
                });
        }

        private static void content(LayoutContainer container)
        {
            container.PaddingTop(10).Column(c =>
            {
                c.Item().Text(t =>
                {
                    t.Style(t => t.Plain);

                    AnalysisInfo analysis = TestData.Analysis;
                    t.Line($"Lab ID: {analysis.Id}");
                    t.Line($"Date: {analysis.Date}");
                    t.Line($"Comment: {analysis.Comment}");
                });

                c.Item()
                    .PaddingTop(10)
                    .Border(b => b.Thickness(1))
                    .PaddingHorizontal(5)
                    .PaddingVertical(3)
                    .Table(t =>
                    {
                        string[] columnNames =
                        {
                            "Analyte",
                            "Units",
                            "Reference",
                            "Result",
                            "Comment"
                        };
                        t.Columns(columns =>
                        {
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(1.5);
                            columns.RelativeColumn(2.5);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        t.Header(h =>
                        {
                            foreach (string columnName in columnNames)
                                h.Cell().TextStyle(t => t.Strong.Underline()).Text(columnName.ToUpperInvariant());
                        });

                        PdfGrayColor gray = new(92);
                        PdfRgbColor red = new(255, 0, 0);
                        PdfRgbColor green = new(0, 255, 0);
                        for (int i = 0; i < TestData.Results.Length; ++i)
                        {
                            void addCell(string? content, PdfColor? textColor = null)
                            {
                                TextSpan text = t.Cell()
                                    .Container(x => i % 2 == 0 ? x.Background(gray) : x)
                                    .Text(content ?? string.Empty);
                                if (textColor != null)
                                    text.FontColor(textColor);
                            }

                            ResultItem result = TestData.Results[i];
                            addCell(result.Analyte);
                            addCell(result.Units);

                            if (result.Reference != null)
                            {
                                var (min, max) = result.Reference.Value;
                                addCell($"{min:N2} - {max:N2}");
                            }
                            else
                            {
                                addCell(null);
                            }

                            ReferenceMatchResult? match = result.FitsReferenceInterval();
                            addCell(result.Result?.ToString("N2"), match != ReferenceMatchResult.Fit ? red : null);

                            string? comment = match switch
                            {
                                ReferenceMatchResult.Less => "less",
                                ReferenceMatchResult.Greater => "greater",
                                ReferenceMatchResult.Fit => "ok",
                                _ => null
                            };
                            addCell(comment, match != ReferenceMatchResult.Fit ? red : green);
                        }
                    });
            });
        }

        private static void footer(LayoutContainer container)
        {
            container.Row(r =>
             {
                 r.AutoItem().Text("Created using Docotic.Pdf.Layout add-on");

                 r.RelativeItem(2).Text(t =>
                 {
                     t.AlignRight();

                     t.Span("Page ");
                     t.CurrentPageNumber();
                     t.Span(" of ");
                     t.PageCount();
                 });
             });
        }
    }
}
