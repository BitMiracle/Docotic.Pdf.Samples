Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    Class HelloWorld
        Public Shared Sub CreatePdf()
            PdfDocumentBuilder.Create().Generate("hello.pdf",
                Sub(doc)
                    doc.Pages(Sub(page) page.Content().Text("Hello, world!"))
                End Sub)
        End Sub
    End Class
End Namespace
