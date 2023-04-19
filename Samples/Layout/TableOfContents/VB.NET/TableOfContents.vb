Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class TableOfContents
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const PathToFile As String = "TableOfContents.pdf"
            PdfDocumentBuilder.Create().Generate(PathToFile,
                Sub(doc)
                    doc.Pages(
                        Sub(page)
                            page.Margin(50)

                            page.Content().Column(
                                Sub(c)
                                    ' Table of contents
                                    c.Item().Text("Contents").Style(Function(t) t.Heading2)
                                    For Each s As Section In Sections
                                        Dim sectionName As String = s.Id
                                        c.Item().SectionLink(sectionName).Row(
                                            Sub(r)
                                                r.AutoItem().Text(s.Title)

                                                Dim pattern = New PdfDashPattern(New Double() {1, 3})
                                                r.RelativeItem().AlignBottom().PaddingHorizontal(3).LineHorizontal(1).DashPattern(pattern)

                                                ' You can also use these methods for different page number types:
                                                ' * t.LastPageNumber(sectionName)
                                                ' * t.PageCount(sectionName)
                                                r.AutoItem().Text(Sub(t) t.FirstPageNumber(sectionName))
                                            End Sub)
                                    Next

                                    ' Sections
                                    For Each s As Section In Sections
                                        c.Item().PageBreak()

                                        c.Item().Section(s.Id).Text(
                                            Sub(t)
                                                t.Line(s.Title).Style(Function(tp) tp.Heading2)

                                                t.Line(s.Content)
                                            End Sub)
                                    Next
                                End Sub)
                        End Sub)
                End Sub)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared ReadOnly Sections = New Section() {
            New Section("extract-text",
                "Extract text from PDF document in C#",
                "Use Docotic.Pdf library to convert PDF documents to text in .NET. You can extract formatted text to parse structured data like tables." + Environment.NewLine + Environment.NewLine +
                "You can also read PDF text with detailed information (position, font, color) about every text chunk. That allows you to search text in PDF documents and highlight found phrases." + Environment.NewLine + Environment.NewLine +
                "Docotic.Pdf supports right-to-left and bidirectional text. You can use it to extract Arabic, Hebrew, and Persian text from PDF documents in .NET."),
            New Section("edit-documents",
                "Edit PDF documents in C#",
                "Docotic.Pdf is a powerful .NET PDF editor. You can compress PDF documents. It is possible to remove content. For example, potentially insecure content like actions, attachments, controls." + Environment.NewLine + Environment.NewLine +
                "You can also edit page objects - replace images, change colors, remove or replace text in PDF." + Environment.NewLine + Environment.NewLine +
                "Docotic.Pdf SDK allows you to split and merge PDF documents in just a few lines of code. And you can remove or reorder pages. With help of the library it's possible to impose PDF pages."),
            New Section("pdf-to-image",
                "Convert PDF to images in C#",
                "Our .NET PDF library allows you to save PDF pages as images. You can convert PDF pages to full size or thumbnail images in PNG, TIFF, and JPEG formats." + Environment.NewLine + Environment.NewLine +
                "Or you can save PDF documents as multipage TIFF files. The library can produce bitonal and grayscale TIFF images." + Environment.NewLine + Environment.NewLine +
                "It is also possible to print PDF documents in C# and VB.NET using Docotic.Pdf." + Environment.NewLine + Environment.NewLine +
                "When needed, you can extract images from PDF documents."),
            New Section("html-to-pdf",
                "Convert HTML to PDF in C#",
                "Generate PDF from HTML using the free HTML to PDF add-on for Docotic.Pdf library." + Environment.NewLine + Environment.NewLine +
                "The add-on uses Chromium during conversion, so the web standards compliance is great. You can produce PDF documents from the most complex HTML documents with scripts and styles." + Environment.NewLine + Environment.NewLine +
                "For produced PDFs, it is possible to set up page size, margins, and orientation. The conversion can be delayed if needed. It is possible to convert password-protected HTML documents and documents with SSL errors.")
        }
    End Class

    NotInheritable Class Section
        Public Sub New(id As String, title As String, content As String)
            Me.Id = id
            Me.Title = title
            Me.Content = content
        End Sub

        Public Id As String
        Public Title As String
        Public Content As String
    End Class
End Namespace
