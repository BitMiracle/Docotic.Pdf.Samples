Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Outline
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "Outline.pdf"

            Using pdf As New PdfDocument()
                pdf.PageMode = PdfPageMode.UseOutlines

                pdf.AddPage()
                pdf.AddPage()

                For i As Integer = 0 To pdf.PageCount - 1
                    Dim canvas As PdfCanvas = pdf.Pages(i).Canvas
                    canvas.DrawString(260, 50, "Page " + (i + 1).ToString())
                Next

                Dim root As PdfOutlineItem = pdf.OutlineRoot
                root.AddChild("Page 1", 0)
                Dim outlineForPage2 As PdfOutlineItem = root.AddChild("Page 2", 1)
                outlineForPage2.AddChild("Page 3", 2)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace