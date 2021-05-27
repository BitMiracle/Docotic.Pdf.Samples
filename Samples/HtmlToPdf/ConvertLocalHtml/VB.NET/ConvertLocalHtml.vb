Imports System
Imports System.IO
Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertLocalHtml
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await convertLocalHtml()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function convertLocalHtml() As Task
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            ' It is possible to use the same converter for multiple conversions.
            ' When reusing the converter you save on converter instantiation time.
            Using converter = Await HtmlConverter.CreateAsync()
                ' Convert some HTML code in a string
                Dim html = "<body><br/><br/><br/><h1>Hello, World<h1></body>"
                Using pdf = Await converter.CreatePdfFromStringAsync(html)
                    pdf.Save("ConvertLocalHtmlString.pdf")
                End Using

                ' Convert a local HTML file
                Dim assemblyPath = AppDomain.CurrentDomain.BaseDirectory
                Dim sampleHtmlPath = Path.Combine(assemblyPath, "sample.html")
                Using pdf = Await converter.CreatePdfAsync(sampleHtmlPath)
                    pdf.Save("ConvertLocalHtmlFile.pdf")
                End Using

                ' Convert some HTML specifying a base URL
                Dim incompleteHtml = "<img src=""/images/team.svg""></img>"
                Dim options = New HtmlConversionOptions()
                options.Load.BaseUri = New Uri("https://bitmiracle.com/")
                Using pdf = Await converter.CreatePdfFromStringAsync(incompleteHtml, options)
                    pdf.Save("ConvertHtmlWithBaseUrl.pdf")
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Function
    End Class
End Namespace
