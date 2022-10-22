Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddXObjectsToLayers
        Private Sub New()
        End Sub
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "AddXObjectsToLayers.pdf"

            Using pdf As New PdfDocument()
                pdf.PageMode = PdfPageMode.UseOC

                Dim firstWatermarkLayer As PdfLayer = pdf.CreateLayer("First XObject")
                Dim secondWatermarkLayer As PdfLayer = pdf.CreateLayer("Second XObject")

                Dim firstObject As PdfXObject = pdf.CreateXObject()
                firstObject.Canvas.DrawString("Text on the first XObject")
                firstObject.Layer = firstWatermarkLayer

                Dim page As PdfPage = pdf.GetPage(0)
                page.Canvas.DrawXObject(firstObject, 0, 100)

                Dim secondObject As PdfXObject = pdf.CreateXObject()
                secondObject.Canvas.DrawString("Text on the second XObject")
                secondObject.Layer = secondWatermarkLayer

                page.Canvas.DrawXObject(secondObject, 100, 200)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
