Imports System.IO

Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertWithHeaderAndFooter
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await ConvertWithHeaderAndFooter()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function ConvertWithHeaderAndFooter() As Task
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "ConvertWithHeaderAndFooter.pdf"

            Using converter = Await HtmlConverter.CreateAsync()
                Dim options = New HtmlConversionOptions()

                options.Page.HeaderTemplate = File.ReadAllText("..\Sample Data\header-template.html")
                options.Page.MarginTop = 110

                options.Page.FooterTemplate = File.ReadAllText("..\Sample Data\footer-template.html")
                options.Page.MarginBottom = 50

                Dim url = New Uri("https://www.iana.org/glossary")
                Using pdf = Await converter.CreatePdfAsync(url, options)
                    pdf.Save(pathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace
