Imports System
Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertWithDelay
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await convertWithDelay()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function convertWithDelay() As Task
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "ConvertWithDelay.pdf"

            ' Wait for 10 seconds before starting the conversion.
            ' This should be enough for the test to complete.
            Dim options = New HtmlConversionOptions()
            options.Start.SetStartAfterDelay(10 * 1000)

            Using converter = Await HtmlConverter.CreateAsync()
                Using pdf = Await converter.CreatePdfAsync(New Uri("https://html5test.com/"), options)
                    pdf.Save(pathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace
