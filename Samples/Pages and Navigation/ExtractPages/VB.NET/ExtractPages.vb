Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExtractPages
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim extractedPagesOutputPath As String = "ExtractPages.pdf"
            Dim remainingPagesOutputPath As String = "ExtractPages_original.pdf"

            Using original As New PdfDocument()
                For i As Integer = 0 To 4
                    If i <> 0 Then
                        original.AddPage()
                    End If

                    Dim page As PdfPage = original.Pages(i)
                    page.Canvas.DrawString("Page #" + (i + 1).ToString())
                Next

                ' extract first 3 pages. These pages will be removed from original PDF document.
                Using extracted As PdfDocument = original.ExtractPages(0, 3)
                    ' Helps to reduce file size in cases when the extracted pages reference
                    ' unused resources such as fonts, images, patterns.
                    extracted.RemoveUnusedResources()

                    extracted.Save(extractedPagesOutputPath)
                End Using

                original.Save(remainingPagesOutputPath)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(extractedPagesOutputPath) With {.UseShellExecute = True})
            Process.Start(New ProcessStartInfo(remainingPagesOutputPath) With {.UseShellExecute = True})

        End Sub
    End Class
End Namespace