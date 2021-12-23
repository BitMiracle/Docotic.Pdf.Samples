Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExtractImageCoordinates
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "ExtractImageCoordinates.pdf"

            Using pdf As New PdfDocument("..\Sample Data\gmail-cheat-sheet.pdf")

                For Each page As PdfPage In pdf.Pages

                    For Each image As PdfPaintedImage In page.GetPaintedImages()
                        Dim canvas As PdfCanvas = page.Canvas
                        canvas.Pen.Width = 3
                        canvas.Pen.Color = New PdfRgbColor(255, 0, 0)

                        canvas.DrawRectangle(image.Bounds)
                    Next

                Next

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace