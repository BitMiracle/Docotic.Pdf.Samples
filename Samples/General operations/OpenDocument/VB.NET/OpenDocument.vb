Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OpenDocument
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "OpenDocument.pdf"

            Using pdf As New PdfDocument("Sample data/jfif3.pdf")
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.Font = pdf.AddFont(PdfBuiltInFont.Helvetica)
                canvas.FontSize = 20
                canvas.DrawString(10, 80, "This text was added by Docotic.Pdf")

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace