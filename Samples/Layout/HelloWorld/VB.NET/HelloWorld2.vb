Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    Class HelloWorld2
        Public Shared Sub CreatePdf()
            PdfDocumentBuilder.Create().Generate("hello.pdf", AddressOf BuildDocument)
        End Sub

        Private Shared Sub BuildDocument(doc As Document)
            doc.Pages(AddressOf BuildPages)
        End Sub

        Private Shared Sub BuildPages(pages As PageLayout)
            pages.Content().Text("Hello, world!")
        End Sub
    End Class
End Namespace
