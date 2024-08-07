Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CopyPages
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "CopyPages.pdf"

            Using pdf As New PdfDocument("..\Sample Data\jfif3.pdf")

                ' copy third and first pages to a new PDF document (page indexes are zero-based)
                Using copy As PdfDocument = pdf.CopyPages(New Integer() {2, 0})
                    ' Helps to reduce file size in cases when the copied pages reference
                    ' unused resources such as fonts, images, patterns.
                    copy.RemoveUnusedResources()

                    copy.Save(pathToFile)
                End Using

            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace