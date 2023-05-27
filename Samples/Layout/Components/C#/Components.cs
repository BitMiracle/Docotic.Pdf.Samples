using System;
using System.Collections.Generic;
using System.Diagnostics;
using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class Components
    {
        static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            const string PathToFile = "Components.pdf";
            PdfDocumentBuilder.Create().Generate(PathToFile, doc =>
            {
                doc.Pages(page =>
                {
                    page.Size(PdfPaperSize.A5)
                        .MarginVertical(50)
                        .MarginHorizontal(40)
                        .Content()
                        .Column(c =>
                        {
                            c.Item().Component(new GoalTable(2022, WorldCupStats.Qatar2022));
                            c.Item().Component(new GoalTable(2018, WorldCupStats.Russia2018));
                        });
                });
            });

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }

    class GoalTable : ILayoutComponent
    {
        private readonly string m_title;
        private readonly TeamStats[] m_data;

        private int m_processedCountries;
        private int m_totalGoals;

        public GoalTable(int year, TeamStats[] data)
        {
            m_title = $"FIFA World Cup {year} total goals";
            m_data = data;
        }

        public void Reset()
        {
            m_processedCountries = 0;
            m_totalGoals = 0;
        }

        public LayoutComponentContent Compose(LayoutContext context)
        {
            LayoutElement e = composeElement(context);
            return new LayoutComponentContent(e, m_processedCountries < m_data.Length);
        }

        private LayoutElement composeElement(LayoutContext context)
        {
            (LayoutElement Element, int SubTotal)? bestResult = null;

            List<TeamStats> countries = new(m_data.Length - m_processedCountries);
            int i;
            for (i = m_processedCountries; i < m_data.Length; ++i)
            {
                int subTotalGoals = 0;
                LayoutElement e = context.CreateElement(container =>
                {
                    container.Column(c =>
                    {
                        if (m_processedCountries == 0)
                            c.Item().Text(m_title).Style(t => t.Heading3);

                        c.Item().Table(t =>
                        {
                            t.Columns(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(4);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                            });

                            t.Header(h =>
                            {
                                string[] columnNames = { "No.", "Country", "Matches", "Goals" };
                                foreach (string column in columnNames)
                                    h.Cell().Background(bg(true)).TextStyle(s => s.Strong).Text(column);
                            });

                            for (int r = m_processedCountries; r <= i; ++r)
                            {
                                TeamStats info = m_data[r];

                                int rowNumber = r + 1;
                                bool alt = (rowNumber - m_processedCountries) % 2 == 0;
                                t.Cell().Background(bg(alt)).Text(rowNumber.ToString());
                                t.Cell().Background(bg(alt)).Text(info.Team);
                                t.Cell().Background(bg(alt)).Text(info.Matches.ToString());
                                t.Cell().Background(bg(alt)).Text(info.Goals.ToString());
                                subTotalGoals += info.Goals;
                            }

                            t.Footer(f =>
                            {
                                f.Cell(c => c.ColumnSpan(4)).AlignRight().Text($"Subtotal goals: {subTotalGoals}");
                            });
                        });

                        if (i == m_data.Length - 1)
                            c.Item().AlignRight().Text($"Total goals: {m_totalGoals + subTotalGoals}").Style(t => t.Strong);
                    });
                });

                if (e.TryFit(context.AvailableSize, out _))
                    bestResult = (e, subTotalGoals);
                else
                    break;
            }

            if (!bestResult.HasValue)
                return context.CreateElement(c => { });

            m_processedCountries = i;

            var (element, subTotal) = bestResult!.Value;
            m_totalGoals += subTotal;
            return element;
        }

        private static PdfColor bg(bool alt)
        {
            return new PdfGrayColor(alt ? 90 : 100);
        }
    }

    record TeamStats(string Team, int Matches, int Goals);

    static class WorldCupStats
    {
        public static readonly TeamStats[] Qatar2022 = new TeamStats[]
        {
            new("France", 7, 16),
            new("Argentina", 7, 15),
            new("England", 5, 13),
            new("Portugal", 5, 12),
            new("Netherlands", 5, 10),
            new("Spain", 4, 9),
            new("Brazil", 5, 8),
            new("Croatia", 7, 8),
            new("Germany", 3, 6),
            new("Morocco", 7, 6),
            new("Japan", 4, 5),
            new("Senegal", 4, 5),
            new("Switzerland", 4, 5),
            new("Ghana", 3, 5),
            new("Serbia", 3, 5),
            new("South Korea", 4, 5),
            new("Australia", 4, 4),
            new("Cameroon", 3, 4),
            new("Ecuador", 3, 4),
            new("Iran", 3, 4),
            new("Costa Rica", 3, 3),
            new("Saudi Arabia", 3, 3),
            new("Poland", 4, 3),
            new("USA", 4, 3),
            new("Canada", 3, 2),
            new("Mexico", 3, 2),
            new("Uruguay", 3, 2),
            new("Belgium", 3, 1),
            new("Denmark", 3, 1),
            new("Qatar", 3, 1),
            new("Tunisia", 3, 1),
            new("Wales", 3, 1),
        };

        public static readonly TeamStats[] Russia2018 = new TeamStats[]
        {
            new("Belgium", 7, 16),
            new("Croatia", 7, 14),
            new("France", 7, 14),
            new("England", 7, 12),
            new("Russia", 5, 11),
            new("Brazil", 5, 8),
            new("Spain", 4, 7),
            new("Uruguay", 5, 7),
            new("Argentina", 4, 6),
            new("Colombia", 4, 6),
            new("Japan", 4, 6),
            new("Portugal", 4, 6),
            new("Sweden", 5, 6),
            new("Tunisia", 3, 5),
            new("Switzerland", 4, 5),
            new("Senegal", 3, 4),
            new("Denmark", 4, 3),
            new("Mexico", 4, 3),
            new("Nigeria", 3, 3),
            new("South Korea", 3, 3),
            new("Australia", 3, 2),
            new("Costa Rica", 3, 2),
            new("Egypt", 3, 2),
            new("Germany", 3, 2),
            new("Iceland", 3, 2),
            new("Iran", 3, 2),
            new("Morocco", 3, 2),
            new("Panama", 3, 2),
            new("Poland", 3, 2),
            new("Peru", 3, 2),
            new("Saudi Arabia", 3, 2),
            new("Serbia", 3, 2),
        };
    }
}
