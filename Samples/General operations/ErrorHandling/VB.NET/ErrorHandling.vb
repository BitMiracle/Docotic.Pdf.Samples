Imports System.Diagnostics
Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ErrorHandling
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "ErrorHandling.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Try
                    ' tries to draw Russian string with built-in Helvetica font which doesn't
                    ' define glyphs for Russian characters. So, we expect that a PdfException will be thrown.
                    page.Canvas.DrawString(10, 50, "Привет, мир!")
                Catch ex As CannotShowTextException
                    page.Canvas.Font = pdf.AddFont("Arial")
                    page.Canvas.DrawString(10, 50, "Expected exception occurred:")
                    page.Canvas.DrawString(10, 65, ex.Message)
                End Try

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace