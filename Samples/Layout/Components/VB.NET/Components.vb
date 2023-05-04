Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class Components
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const PathToFile As String = "Components.pdf"
            PdfDocumentBuilder.Create().Generate(PathToFile,
                Sub(doc)
                    doc.Pages(
                        Sub(page)
                            page.Size(PdfPaperSize.A5)
                            page.MarginVertical(50)
                            page.MarginHorizontal(40)

                            page.Content().Column(
                                Sub(c)
                                    c.Item().Component(New GoalTable(2022, WorldCupStats.Qatar2022))
                                    c.Item().Component(New GoalTable(2018, WorldCupStats.Russia2018))
                                End Sub)
                        End Sub)
                End Sub)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class

    Class GoalTable
        Implements ILayoutComponent

        Private ReadOnly m_title As String
        Private ReadOnly m_data As TeamStats()
        Private m_processedCountries As Integer
        Private m_totalGoals As Integer

        Public Sub New(year As Integer, data As TeamStats())
            m_title = $"FIFA World Cup {year} total goals"
            m_data = data
        End Sub

        Public Sub Reset() Implements ILayoutComponent.Reset
            m_processedCountries = 0
            m_totalGoals = 0
        End Sub

        Public Function Compose(context As LayoutContext) As LayoutComponentContent Implements ILayoutComponent.Compose
            Dim e As LayoutElement = composeElement(context)
            Return New LayoutComponentContent(e, m_processedCountries < m_data.Length)
        End Function

        Private Function composeElement(context As LayoutContext) As LayoutElement
            Dim bestResult As (Element As LayoutElement, SubTotal As Integer)? = Nothing

            Dim countries = New List(Of TeamStats)(m_data.Length - m_processedCountries)
            Dim i As Integer

            For i = m_processedCountries To m_data.Length - 1
                Dim subTotalGoals As Integer = 0
                Dim e As LayoutElement = context.CreateElement(
                    Sub(container)
                        container.Column(
                            Sub(c)
                                If m_processedCountries = 0 Then c.Item().Text(m_title).Style(Function(t) t.Heading3)
                                c.Item().Table(
                                    Sub(t)
                                        t.Columns(
                                            Sub(col)
                                                col.RelativeColumn(1)
                                                col.RelativeColumn(4)
                                                col.RelativeColumn(2)
                                                col.RelativeColumn(2)
                                            End Sub)

                                        t.Header(
                                            Sub(h)
                                                Dim columnNames As String() = {"No.", "Country", "Matches", "Goals"}

                                                For Each column As String In columnNames
                                                    h.Cell().Background(bg(True)).TextStyle(Function(s) s.Strong).Text(column)
                                                Next
                                            End Sub)

                                        For r As Integer = m_processedCountries To i
                                            Dim info As TeamStats = m_data(r)
                                            Dim rowNumber As Integer = r + 1
                                            Dim alt As Boolean = (rowNumber - m_processedCountries) Mod 2 = 0
                                            t.Cell().Background(bg(alt)).Text(rowNumber.ToString())
                                            t.Cell().Background(bg(alt)).Text(info.Team)
                                            t.Cell().Background(bg(alt)).Text(info.Matches.ToString())
                                            t.Cell().Background(bg(alt)).Text(info.Goals.ToString())
                                            subTotalGoals += info.Goals
                                        Next

                                        t.Footer(
                                            Sub(f)
                                                f.Cell(Sub(cell) cell.ColumnSpan(4)).AlignRight().Text($"Subtotal goals: {subTotalGoals}")
                                            End Sub)
                                    End Sub)

                                If i = m_data.Length - 1 Then
                                    c.Item().AlignRight().Text($"Total goals: {m_totalGoals + subTotalGoals}").Style(Function(t) t.Strong)
                                End If
                            End Sub)
                    End Sub)

                Dim actualSize As PdfSize
                If e.TryFit(context.AvailableSize, actualSize) Then
                    bestResult = (e, subTotalGoals)
                Else
                    Exit For
                End If
            Next

            If Not bestResult.HasValue Then
                Return context.CreateElement(
                    Sub(c)
                    End Sub)
            End If

            m_processedCountries = i
            m_totalGoals += bestResult.Value.SubTotal
            Return bestResult.Value.Element
        End Function

        Private Shared Function bg(alt As Boolean) As PdfColor
            Return New PdfGrayColor(If(alt, 90, 100))
        End Function
    End Class

    NotInheritable Class TeamStats
        Public Sub New(team As String, matches As Integer, goals As Integer)
            Me.Team = team
            Me.Matches = matches
            Me.Goals = goals
        End Sub

        Public Team As String
        Public Matches As Integer
        Public Goals As Integer
    End Class

    Class WorldCupStats
        Public Shared ReadOnly Qatar2022 = New TeamStats() {
            New TeamStats("France", 7, 16),
            New TeamStats("Argentina", 7, 15),
            New TeamStats("England", 5, 13),
            New TeamStats("Portugal", 5, 12),
            New TeamStats("Netherlands", 5, 10),
            New TeamStats("Spain", 4, 9),
            New TeamStats("Brazil", 5, 8),
            New TeamStats("Croatia", 7, 8),
            New TeamStats("Germany", 3, 6),
            New TeamStats("Morocco", 7, 6),
            New TeamStats("Japan", 4, 5),
            New TeamStats("Senegal", 4, 5),
            New TeamStats("Switzerland", 4, 5),
            New TeamStats("Ghana", 3, 5),
            New TeamStats("Serbia", 3, 5),
            New TeamStats("South Korea", 4, 5),
            New TeamStats("Australia", 4, 4),
            New TeamStats("Cameroon", 3, 4),
            New TeamStats("Ecuador", 3, 4),
            New TeamStats("Iran", 3, 4),
            New TeamStats("Costa Rica", 3, 3),
            New TeamStats("Saudi Arabia", 3, 3),
            New TeamStats("Poland", 4, 3),
            New TeamStats("USA", 4, 3),
            New TeamStats("Canada", 3, 2),
            New TeamStats("Mexico", 3, 2),
            New TeamStats("Uruguay", 3, 2),
            New TeamStats("Belgium", 3, 1),
            New TeamStats("Denmark", 3, 1),
            New TeamStats("Qatar", 3, 1),
            New TeamStats("Tunisia", 3, 1),
            New TeamStats("Wales", 3, 1)
        }

        Public Shared ReadOnly Russia2018 = New TeamStats() {
            New TeamStats("Belgium", 7, 16),
            New TeamStats("Croatia", 7, 14),
            New TeamStats("France", 7, 14),
            New TeamStats("England", 7, 12),
            New TeamStats("Russia", 5, 11),
            New TeamStats("Brazil", 5, 8),
            New TeamStats("Spain", 4, 7),
            New TeamStats("Uruguay", 5, 7),
            New TeamStats("Argentina", 4, 6),
            New TeamStats("Colombia", 4, 6),
            New TeamStats("Japan", 4, 6),
            New TeamStats("Portugal", 4, 6),
            New TeamStats("Sweden", 5, 6),
            New TeamStats("Tunisia", 3, 5),
            New TeamStats("Switzerland", 4, 5),
            New TeamStats("Senegal", 3, 4),
            New TeamStats("Denmark", 4, 3),
            New TeamStats("Mexico", 4, 3),
            New TeamStats("Nigeria", 3, 3),
            New TeamStats("South Korea", 3, 3),
            New TeamStats("Australia", 3, 2),
            New TeamStats("Costa Rica", 3, 2),
            New TeamStats("Egypt", 3, 2),
            New TeamStats("Germany", 3, 2),
            New TeamStats("Iceland", 3, 2),
            New TeamStats("Iran", 3, 2),
            New TeamStats("Morocco", 3, 2),
            New TeamStats("Panama", 3, 2),
            New TeamStats("Poland", 3, 2),
            New TeamStats("Peru", 3, 2),
            New TeamStats("Saudi Arabia", 3, 2),
            New TeamStats("Serbia", 3, 2)
        }
    End Class
End Namespace
