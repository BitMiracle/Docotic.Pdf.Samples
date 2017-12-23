Imports System.Diagnostics
Imports System.Drawing
Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CreateLayers
        Private Sub New()
        End Sub
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

			Dim pathToFile As String = "CreateLayers.pdf"

            Using pdf As New PdfDocument()
                pdf.PageMode = PdfPageMode.UseOC

                Dim firstLayer As PdfLayer = pdf.CreateLayer("First Layer")
                firstLayer.Visible = False

                Dim secondLayer As PdfLayer = pdf.CreateLayer("Second Layer", False, PdfLayerIntent.View)
                secondLayer.Visible = True

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace
