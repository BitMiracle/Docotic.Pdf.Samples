Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SvgToPdf
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

            Using converter = Await HtmlConverter.CreateAsync()
                Using pdf = Await converter.CreatePdfAsync("..\Sample Data\image.svg")
                    pdf.Save("SvgToPdf.pdf")
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Function
    End Class
End Namespace
