Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    Class HelloWorld4
        Public Shared Sub CreatePdf()
            PdfDocumentBuilder.Create().Generate("hello.pdf", AddressOf BuildDocument)
        End Sub

        Private Shared Sub BuildDocument(doc As Document)
            doc.Typography(Sub(t) t.Document = doc.TextStyleWithFont(SystemFont.Family("Calibri")))
            doc.Pages(AddressOf BuildPages)
        End Sub

        Private Shared Sub BuildPages(pages As PageLayout)
            pages.Size(PdfPaperSize.A6)
            pages.Margin(10)
            BuildPagesHeader(pages.Header())
            BuildPagesFooter(pages.Footer())
            BuildTextContent(pages.Content())
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
