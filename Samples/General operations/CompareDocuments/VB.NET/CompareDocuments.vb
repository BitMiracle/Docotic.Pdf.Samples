Imports System.Text
Imports BitMiracle.Docotic

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CompareDocuments
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const originalFile As String = "..\Sample Data\Link.pdf"
            Using first As New PdfDocument(originalFile)
                first.Save("first.pdf")
            End Using

            Using second As New PdfDocument(originalFile)
                second.Save("second.pdf")
            End Using

            Using third As New PdfDocument(originalFile)
                third.Pages(0).Canvas.DrawString("Hello")
                third.Save("third.pdf")
            End Using

            Dim message As New StringBuilder()

            message.AppendLine("first.pdf equals to second.pdf?")
            If PdfDocument.DocumentsAreEqual("first.pdf", "second.pdf") Then
                message.AppendLine("Yes")
            Else
                message.AppendLine("No")
            End If

            message.AppendLine()

            message.AppendLine("first.pdf equals to third.pdf?")
            If PdfDocument.DocumentsAreEqual("first.pdf", "third.pdf") Then
                message.AppendLine("Yes")
            Else
                message.AppendLine("No")
            End If

            Console.WriteLine(message.ToString())
        End Sub
    End Class
End Namespace