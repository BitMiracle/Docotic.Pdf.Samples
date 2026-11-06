Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddAttachments
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "AddAttachments.pdf"

            Using pdf As New PdfDocument()
                ' Add a shared attachment to the document.
                Dim ammerland As PdfFileSpecification = pdf.CreateFileAttachment("..\Sample Data\ammerland.jpg")
                pdf.SharedAttachments.Add(ammerland.Specification, ammerland)

                ' Add a file attachment annotation to the first page.
                Dim bounds As New PdfRectangle(20, 70, 100, 100)
                Dim jpeg As PdfFileSpecification = pdf.CreateFileAttachment("..\Sample Data\jpeg.pdf")
                pdf.Pages(0).AddFileAnnotation(bounds, jpeg)

                pdf.PageMode = PdfPageMode.UseAttachments

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace