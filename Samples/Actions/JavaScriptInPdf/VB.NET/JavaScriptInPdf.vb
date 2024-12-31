Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class JavaScriptInPdf
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            ' CreateNamesDocument.Run()

            HelloWorld()
            ValidateData.Run()
            SynchronizeData.Run()

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

        End Sub

        Private Shared Sub HelloWorld()
            Dim pathToFile As String = "HelloWorld.pdf"
            Using pdf As New PdfDocument()
                pdf.OnOpenDocument = pdf.CreateJavaScriptAction("app.alert(""Hello, Medium!"", 3);")

                pdf.Save(pathToFile)
            End Using

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace