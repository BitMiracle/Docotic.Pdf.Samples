Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class PutHtmlOverPdf
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await putHtmlOverPdf()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function putHtmlOverPdf() As Task
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "PutHtmlOverPdf.pdf"

            Using converter = Await HtmlConverter.CreateAsync()
                ' It Is important to specify page size And turn on transparent background.
                ' Usually, the size should be equal to the size of the page you want to overlay.
                Dim options = New HtmlConversionOptions()
                options.Page.TransparentBackground = True
                options.Page.SetSizeInches(4.13, 5.83)

                Dim htmlCode = "<div style=""position: absolute; top: 270px; right: 100px;\"">" +
                    "I would like to put this here</div>"
                Using htmlPdf = Await converter.CreatePdfFromStringAsync(htmlCode, options)
                    Using pdf = New PdfDocument("..\Sample Data\simple-graphics.pdf")
                        ' Create an XObject from a page in the document generated from HTML.
                        ' Please note that the code uses different document instances to call the
                        ' method And access the collection of the pages.
                        Dim xObj = pdf.CreateXObject(htmlPdf.Pages(0))

                        ' Draw the XObject on a page from the existing PDF.
                        pdf.Pages(0).Canvas.DrawXObject(xObj, 0, 0)

                        pdf.Save(pathToFile)
                    End Using
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace
