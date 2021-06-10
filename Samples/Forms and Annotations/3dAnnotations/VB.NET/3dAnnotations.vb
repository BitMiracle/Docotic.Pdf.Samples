Imports System.Diagnostics
Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ThreeDAnnotations
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const PathToFile As String = "3dAnnotations.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Dim bounds As PdfRectangle = New PdfRectangle(10, 80, 400, 400)
                Dim annot As Pdf3dAnnotation = page.Add3dAnnotation(bounds, "Sample Data\dice.u3d")
                annot.Activation.ActivationMode = PdfRichMediaActivationMode.OnPageOpen

                pdf.Save(PathToFile)
            End Using

            Process.Start(PathToFile)
        End Sub
    End Class
End Namespace