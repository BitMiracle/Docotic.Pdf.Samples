Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Watermarks
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Watermarks.pdf"

            Using pdf As New PdfDocument()
                pdf.AddPage()

                Dim watermark As PdfXObject = pdf.CreateXObject()

                ' uncomment following line if you want the watermark to be placed before
                ' any other contents of a page.
                ' watermark.DrawOnBackground = True

                Dim watermarkCanvas As PdfCanvas = watermark.Canvas
                watermarkCanvas.TextAngle = -45
                watermarkCanvas.FontSize = 36
                watermarkCanvas.DrawString(100, 100, "This text is a part of the watermark")

                For i As Integer = 0 To pdf.PageCount - 1
                    ' draw watermark at the top left corner over the page contents
                    ' this will also add watermark to the collection of page XObjects
                    pdf.Pages(i).Canvas.DrawXObject(watermark, 0, 0)
                Next

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace