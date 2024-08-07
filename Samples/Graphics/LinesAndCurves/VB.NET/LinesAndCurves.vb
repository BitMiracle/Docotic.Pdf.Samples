Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class LinesAndCurves
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "LinesAndCurves.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                'draw star
                Dim startPosition As New PdfPoint(30, 80)
                canvas.CurrentPosition = startPosition
                canvas.DrawLineTo(110, 80)
                canvas.DrawLineTo(45, 110)
                canvas.DrawLineTo(70, 60)
                canvas.DrawLineTo(95, 110)
                canvas.DrawLineTo(startPosition)

                'draw bezier curve
                canvas.CurrentPosition = New PdfPoint(150, 90)
                canvas.DrawCurveTo(New PdfPoint(180, 60), New PdfPoint(230, 120), New PdfPoint(250, 90))

                'draw arc
                canvas.DrawArc(New PdfPoint(350, 90), 70, 40, 30, 280)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace