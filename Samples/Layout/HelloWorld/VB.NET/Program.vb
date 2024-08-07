Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class HelloWorldSample
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            HelloWorld7.CreatePdf()

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo("hello.pdf") With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
