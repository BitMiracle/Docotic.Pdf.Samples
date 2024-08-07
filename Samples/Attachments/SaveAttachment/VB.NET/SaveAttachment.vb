Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SaveAttachment
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Using pdf As New PdfDocument("..\Sample Data\Attachments.pdf")
                Dim spec As PdfFileSpecification = pdf.SharedAttachments("File Attachment testing.doc")
                If spec IsNot Nothing And spec.Contents IsNot Nothing Then
                    Const pathToFile As String = "attachment.doc"
                    spec.Contents.Save(pathToFile)

                    Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

                    Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
                Else
                    Console.WriteLine($"Can't save the shared attachment")
                End If
            End Using
        End Sub
    End Class
End Namespace