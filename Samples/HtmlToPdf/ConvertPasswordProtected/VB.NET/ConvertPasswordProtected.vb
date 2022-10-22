Imports System
Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertPasswordProtected
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await convertPasswordProtected()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function convertPasswordProtected() As Task
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "ConvertPasswordProtected.pdf"
            Using converter = Await HtmlConverter.CreateAsync()
                Dim url = New Uri("http://httpbin.org/basic-auth/foo/bar")

                Dim options = New HtmlConversionOptions()
                options.Authentication.SetCredentials("foo", "bar")

                ' The following two lines are here just to make the output easier to read.
                options.Page.MarginTop = 100
                options.Page.Scale = 2

                Using pdf = Await converter.CreatePdfAsync(url, options)
                pdf.Save(pathToFile)
            End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace
