Imports System.Diagnostics
Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CreateAndSaveDocument
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "CreateAndSaveDocument.pdf"

            Using pdf As New PdfDocument()

                ' let's draw "Hello, world" on the first page
                Dim firstPage As PdfPage = pdf.Pages(0)
                firstPage.Canvas.DrawString(10, 50, "Hello, world!")

                pdf.Save(pathToFile)
            End Using

            ' opens saved document in default PDF viewer
            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace