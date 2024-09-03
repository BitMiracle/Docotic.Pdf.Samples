Imports System
Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertPasswordProtected
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await ConvertPasswordProtected()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function ConvertPasswordProtected() As Task
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "ConvertPasswordProtected.pdf"
            Using converter = Await HtmlConverter.CreateAsync()
                Dim url = New Uri("http://httpbin.org/basic-auth/foo/bar")

                Dim options = New HtmlConversionOptions()
                options.Authentication.SetCredentials("foo", "bar")

                Using pdf = Await converter.CreatePdfAsync(url, options)
                    pdf.Save(pathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace
