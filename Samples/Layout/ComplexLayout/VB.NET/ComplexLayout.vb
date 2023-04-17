Imports System.IO
Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class ComplexLayout
        Public Shared Sub Main()
            Const PathToFile As String = "ComplexLayout.pdf"
            With PdfDocumentBuilder.Create()
                .Info(
                    Sub(info)
                        info.Title = "Complex layout example"
                        info.Creator = "Docotic.Pdf samples"
                        info.Author = TestData.CompanyName
                        info.CreationDate = TestData.CreatedOn
                    End Sub)
                .Generate(PathToFile, Sub(doc) generate(doc))
            End With

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub generate(doc As Document)
            doc.Typography(
                Sub(t)
                    Dim roboto = New FileInfo("..\Sample Data\Fonts\Roboto\Roboto-Regular.ttf")
                    t.Document = doc.TextStyleWithFont(roboto)
                    t.Header = t.Parent.FontSize(8)

                    Dim robotoBold = New FileInfo("..\Sample Data\Fonts\Roboto\Roboto-Bold.ttf")
                    t.Strong = doc.TextStyleWithFont(robotoBold)
                End Sub)

            Dim logo As Image = doc.Image(New FileInfo("..\Sample Data\logo.png"))

            doc.Pages(
                Sub(page)
                    page.MarginVertical(20)
                    page.MarginHorizontal(40)

                    header(page.Header(), logo)

                    content(page.Content())

                    footer(page.Footer())
                End Sub)
        End Sub

        Private Shared Sub header(container As LayoutContainer, logo As Image)
            container.Column(
                Sub(c)
                    c.Item().Row(
                        Sub(r)
                            r.AutoItem().Height(50).Width(50).Image(logo)
                            r.RelativeItem(1).AlignRight().Text($"Created: {TestData.CreatedOn:yyyy/MM/dd HH:mm:ss}")
                        End Sub)

                    c.Item().Border(Sub(b) b.Bottom(1)).PaddingBottom(5).Row(
                        Sub(r)
                            r.RelativeItem(4).Text(TestData.CompanyName)
                            r.RelativeItem(4).AlignCenter().Text($"Phone: {TestData.Phone}")
                            r.RelativeItem(4).AlignRight().Text($"Email: {TestData.Email}")
                        End Sub)

                    c.Item().PaddingTop(5).Row(
                        Sub(r)
                            For i As Integer = 0 To 1
                                Dim index = i
                                r.RelativeItem(6).PaddingRight(8).Layers(
                                    Sub(l)
                                        l.Layer().TranslateX(5).TranslateY(5).Background(New PdfGrayColor(50))

                                        l.PrimaryLayer().Border(Sub(b) b.Thickness(1)).Background(New PdfGrayColor(100)).Padding(5).Text(
                                            Sub(t)
                                                If index = 0 Then
                                                    Dim patient As PatientInfo = TestData.Patient
                                                    t.Line($"Patient name: {patient.Name}")
                                                    t.Line($"Date of birth: {patient.BirthDate:yyyy/MM/dd}")
                                                    t.Line($"Gender: {patient.Gender}")
                                                Else
                                                    Dim clinic As ClinicInfo = TestData.Clinic
                                                    t.Line($"Clinic: {clinic.Name}")
                                                    t.Line($"Address: {clinic.Address}")
                                                End If
                                            End Sub)
                                    End Sub)
                            Next
                        End Sub)
                End Sub)
        End Sub

        Private Shared Sub content(container As LayoutContainer)
            container.PaddingTop(10).Column(
                Sub(c)
                    c.Item().Text(
                        Sub(t)
                            t.Style(Function(tp) tp.Plain)

                            Dim analysis = TestData.Analysis
                            t.Line($"Lab ID: {analysis.Id}")
                            t.Line($"Date: {analysis.AnalysisDate}")
                            t.Line($"Comment: {analysis.Comment}")
                        End Sub)

                    c.Item().PaddingTop(10).Border(Function(b) b.Thickness(1)).PaddingHorizontal(5).PaddingVertical(3).Table(
                        Sub(t)
                            Dim columnNames As String() = {"Analyte", "Units", "Reference", "Result", "Comment"}
                            t.Columns(
                                Sub(columns)
                                    columns.RelativeColumn(4)
                                    columns.RelativeColumn(1.5)
                                    columns.RelativeColumn(2.5)
                                    columns.RelativeColumn(2)
                                    columns.RelativeColumn(2)
                                End Sub)

                            t.Header(
                                Sub(h)
                                    For Each columnName As String In columnNames
                                        h.Cell().TextStyle(Function(tp) tp.Strong.Underline()).Text(columnName.ToUpperInvariant())
                                    Next
                                End Sub)

                            Dim gray = New PdfGrayColor(92)
                            Dim red = New PdfRgbColor(255, 0, 0)
                            Dim green = New PdfRgbColor(0, 255, 0)
                            For i As Integer = 0 To TestData.Results.Length - 1
                                Dim background As PdfColor = If(i Mod 2 = 0, gray, Nothing)

                                Dim result As ResultItem = TestData.Results(i)
                                addCell(t, result.Analyte, background)
                                addCell(t, result.Units, background)

                                If result.Reference IsNot Nothing Then
                                    Dim r = result.Reference.Value
                                    addCell(t, $"{r.Min:N2} - {r.Max:N2}", background)
                                Else
                                    addCell(t, Nothing, background)
                                End If

                                Dim match = result.FitsReferenceInterval()
                                addCell(t, result.Result?.ToString("N2"), background, If(match <> ReferenceMatchResult.Fit, red, Nothing))

                                Dim comment = formatComment(match)
                                addCell(t, comment, background, If(match <> ReferenceMatchResult.Fit, red, green))
                            Next
                        End Sub)
                End Sub)
        End Sub

        Private Shared Sub addCell(t As Table, content As String, background As PdfColor,
                Optional textColor As PdfColor = Nothing)
            Dim cell As LayoutContainer = t.Cell()
            If background IsNot Nothing Then
                cell = cell.Background(background)
            End If

            Dim text As TextSpan = cell.Text(If(content, String.Empty))
            If textColor IsNot Nothing Then text.FontColor(textColor)
        End Sub

        Private Shared Function formatComment(match As ReferenceMatchResult?) As String
            Select Case match
                Case ReferenceMatchResult.Less
                    Return "less"
                Case ReferenceMatchResult.Greater
                    Return "greater"
                Case ReferenceMatchResult.Fit
                    Return "ok"
            End Select

            Return Nothing
        End Function

        Private Shared Sub footer(container As LayoutContainer)
            container.Row(
                Sub(r)
                    r.AutoItem().Text("Created using Docotic.Pdf.Layout add-on")

                    r.RelativeItem(2).Text(
                        Sub(t)
                            t.AlignRight()
                            t.Span("Page ")
                            t.CurrentPageNumber()
                            t.Span(" of ")
                            t.PageCount()
                        End Sub)
                End Sub)
        End Sub
    End Class
End Namespace
