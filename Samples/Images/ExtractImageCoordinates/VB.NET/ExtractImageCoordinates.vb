Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExtractImageCoordinates
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

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

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace