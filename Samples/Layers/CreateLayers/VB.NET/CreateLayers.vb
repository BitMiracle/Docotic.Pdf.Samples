Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CreateLayers
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "CreateLayers.pdf"

            Using pdf As New PdfDocument()
                pdf.PageMode = PdfPageMode.UseOC

                Dim firstLayer As PdfLayer = pdf.CreateLayer("First Layer")
                firstLayer.Visible = False

                Dim secondLayer As PdfLayer = pdf.CreateLayer("Second Layer", False, PdfLayerIntent.View)
                secondLayer.Visible = True

                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                canvas.BeginMarkedContent(PdfTagType.OC, firstLayer)
                canvas.DrawString(10, 50, "First")
                canvas.EndMarkedContent()

                canvas.BeginMarkedContent(PdfTagType.OC, secondLayer)
                canvas.DrawString(10, 70, "Second")
                canvas.EndMarkedContent()

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
