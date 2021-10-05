Imports System
Imports System.IO
Imports System.Reflection
Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertWithHeaderAndFooter
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await convertWithHeaderAndFooter()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function convertWithHeaderAndFooter() As Task
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using converter = Await HtmlConverter.CreateAsync()
                Dim options = New HtmlConversionOptions()

                Dim location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                options.Page.HeaderTemplate = File.ReadAllText(Path.Combine(location, "header-template.html"))
                options.Page.MarginTop = 110

                options.Page.FooterTemplate = File.ReadAllText(Path.Combine(location, "footer-template.html"))
                options.Page.MarginBottom = 50

                Dim url = New Uri("https://www.iana.org/glossary")
                Using pdf = Await converter.CreatePdfAsync(url, options)
                    pdf.Save("ConvertWithHeaderAndFooter.pdf")
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Function
    End Class
End Namespace
