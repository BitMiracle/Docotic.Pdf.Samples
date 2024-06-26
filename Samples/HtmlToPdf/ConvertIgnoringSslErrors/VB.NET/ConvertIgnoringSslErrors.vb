Imports System
Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertIgnoringSslErrors
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await ConvertIgnoringSslErrors()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function ConvertIgnoringSslErrors() As Task
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "ConvertIgnoringSslErrors.pdf"

            Dim engineOptions = New HtmlEngineOptions With {
                .IgnoreSslErrors = True
            }

            Using converter = Await HtmlConverter.CreateAsync(engineOptions)
                Dim url = New Uri("https://self-signed.badssl.com/")

                Using pdf = Await converter.CreatePdfAsync(url)
                    pdf.Save(pathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace
