Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class DrawText
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "DrawText.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                canvas.DrawString(10, 50, "Hello, world!")

                Const longString As String = "Lorem ipsum dolor sit amet, consectetur adipisicing elit"
                canvas.DrawString(longString, New PdfRectangle(10, 70, 40, 150), PdfTextAlign.Left, PdfVerticalAlign.Top)
                canvas.DrawText(longString, New PdfRectangle(70, 70, 40, 150), PdfTextAlign.Left, PdfVerticalAlign.Top)

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace
