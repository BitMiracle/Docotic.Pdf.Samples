Imports System.Diagnostics
Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class TextAnnotations
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "TextAnnotations.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)
                Const content As String = "Lorem ipsum dolor sit amet, consectetur adipisicing elit"
                Dim textAnnotation As PdfTextAnnotation = page.AddTextAnnotation(10, 50, "Docotic.Pdf Samples", content)
                textAnnotation.Icon = PdfTextAnnotationIcon.Comment
                textAnnotation.Opened = True

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace