Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddSingleImageFrame
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "AddSingleImageFrame.pdf"

            Using pdf As New PdfDocument()
                Dim frames As PdfImageFrames = pdf.OpenImage("..\Sample Data\multipage.tif")
                Dim secondFrame As PdfImageFrame = frames(1)

                Dim image As PdfImage = pdf.CreateImage(secondFrame)
                pdf.Pages(0).Canvas.DrawImage(image, 10, 10, 0)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace