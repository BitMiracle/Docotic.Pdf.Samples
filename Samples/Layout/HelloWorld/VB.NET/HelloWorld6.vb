Imports System.Globalization
Imports System.IO
Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    Class HelloWorld6
        Public Shared Sub CreatePdf()
            PdfDocumentBuilder.Create().Generate("hello.pdf", AddressOf BuildDocument)
        End Sub

        Private Shared Sub BuildDocument(doc As Document)
            doc.Typography(Sub(t) t.Document = doc.TextStyleWithFont(SystemFont.Family("Calibri")))

            Dim imageFile = New FileInfo("..\Sample Data\red-flowers-at-butterfly-world.jpg")
            Dim image = doc.Image(imageFile)

            doc.Pages(
                Sub(pages)
                    BuildPages(pages)

                    pages.Content().Column(
                        Sub(c)
                            BuildTextContent(c.Item())
                            BuildImageContent(c.Item(), image)
                            BuildListContent(c.Item())
                        End Sub)
                End Sub)
        End Sub

        Private Shared Sub BuildListContent(content As LayoutContainer)
            Dim dayNames = DateTimeFormatInfo.InvariantInfo.DayNames
            Dim dayNamesSpain = DateTimeFormatInfo.GetInstance(New CultureInfo("es-ES")).DayNames

            content.Column(
                Sub(column)
                    For i As Integer = 0 To dayNames.Length - 1
                        Dim rowIndex = i
                        column.Item().Row(
                            Sub(row)
                                row.Spacing(5)
                                row.AutoItem().Text($"{rowIndex + 1}.")
                                row.RelativeItem().Text(
                                    Sub(t)
                                        t.Line(dayNames(rowIndex))
                                        t.Line($"In Spain they call it {dayNamesSpain(rowIndex)}")
                                    End Sub)
                            End Sub)
                    Next
                End Sub)
        End Sub

        Private Shared Sub BuildImageContent(content As LayoutContainer, image As Image)
            content.AlignCenter().
                PaddingVertical(20).
                Image(image)
        End Sub

        Private Shared Sub BuildPages(pages As PageLayout)
            pages.Size(PdfPaperSize.A6)
            pages.Margin(10)
            BuildPagesHeader(pages.Header())
            BuildPagesFooter(pages.Footer())
        End Sub

        Private Shared Sub BuildTextContent(content As LayoutContainer)
            Dim colorAccented = New PdfRgbColor(56, 194, 10)
            Dim styleAccented = TextStyle.Parent.FontColor(colorAccented)
            content.Text(
                Sub(t)
                    t.Span("Hello, ").Style(styleAccented)
                    t.Span("world!").Style(styleAccented.Underline())
                End Sub)
        End Sub

        Private Shared Sub BuildPagesHeader(header As LayoutContainer)
            header.TextStyle(TextStyle.Parent.FontSize(8)).
                AlignRight().Text(
                    Sub(t)
                        t.Line($"Created by: {Environment.UserName}")
                        t.Line($"Date: {DateTime.Now}")
                    End Sub)
        End Sub

        Private Shared Sub BuildPagesFooter(footer As LayoutContainer)
            footer.Height(20).
                Background(New PdfGrayColor(95)).
                AlignCenter().
                Text(Sub(t) t.CurrentPageNumber())
        End Sub
    End Class
End Namespace
