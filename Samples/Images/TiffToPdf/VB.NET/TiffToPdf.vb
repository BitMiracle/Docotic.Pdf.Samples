Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public Class TiffToPdf
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "TiffToPdf.pdf"

            Using pdf As New PdfDocument
                Dim tiffFiles() As String = New String() {
                    "..\Sample Data\multipage.tif",
                    "..\Sample Data\pinkMask.tif"
                }
                Dim imagesAdded As Integer = 0
                For Each tiff As String In tiffFiles
                    ' Open a TIFF image that may contain multiple pages.
                    Dim frames As PdfImageFrames = pdf.OpenImage(tiff)
                    Dim pages = pdf.Pages
                    For Each frame As PdfImageFrame In frames
                        Dim pdfPage As PdfPage = If(imagesAdded = 0, pages(0), pages.Add())

                        Dim image As PdfImage = pdf.CreateImage(frame)
                        ' Set the page size to match the image size so that the image covers the
                        ' entire page.
                        pdfPage.Height = image.Height
                        pdfPage.Width = image.Width
                        pdfPage.Canvas.DrawImage(image, 0, 0, pdfPage.Width, pdfPage.Height, 0)
                        imagesAdded += 1
                    Next
                Next
                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace