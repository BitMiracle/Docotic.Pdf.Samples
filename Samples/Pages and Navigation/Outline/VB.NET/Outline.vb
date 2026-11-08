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

                pdf.Pages.Add()
                pdf.Pages.Add()

                For i As Integer = 0 To pdf.Pages.Count - 1
                    Dim canvas As PdfCanvas = pdf.Pages(i).Canvas
                    canvas.DrawString(260, 50, "Page " + (i + 1).ToString())
                Next

                Dim outlines = pdf.Outlines

                Dim page1Outline = outlines.Add("Page 1")
                page1Outline.View = pdf.CreateView(0)

                Dim page2Outline = outlines.Add("Page 2")
                page2Outline.View = pdf.CreateView(1)

                Dim page3Outline = page2Outline.Children.Add("Page 3")
                page3Outline.View = pdf.CreateView(2)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace