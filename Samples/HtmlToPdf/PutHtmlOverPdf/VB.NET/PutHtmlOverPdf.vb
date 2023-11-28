Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class PutHtmlOverPdf
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await PutHtmlOverPdf()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function PutHtmlOverPdf() As Task
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "PutHtmlOverPdf.pdf"

            Using converter = Await HtmlConverter.CreateAsync()
                ' It is important to specify page size. Usually, the size should be equal to the size of the page
                ' you want to overlay.
                Dim options = New HtmlConversionOptions()
                options.Page.SetSizeInches(4.13, 5.83)

                ' The library produces transparent background by default. If you need an opaque background then you can
                ' use CSS background like that:
                'options.Start.SetStartAfterScriptRun("document.body.style.background = 'white'")
                '
                ' Alternatively, you can add a white background watermark to all converted pages.
                ' Look at the addWhiteBackground method below for more detail.

                Dim htmlCode = "<div style=""position: absolute; top: 270px; right: 100px;\"">" +
                    "I would like to put this here</div>"
                Using htmlPdf = Await converter.CreatePdfFromStringAsync(htmlCode, options)
                    ' Uncomment to apply semi-transparent red background:
                    'AddBackground(htmlPdf, New PdfRgbColor(255, 0, 0), 25)

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

        Private Shared Sub AddBackground(pdf As PdfDocument, color As PdfColor, opacity As Integer)
            Dim background As PdfXObject = pdf.CreateXObject()
            background.DrawOnBackground = True

            Dim first = True
            For Each page As PdfPage In pdf.Pages
                If first Then
                    background.Width = page.Width
                    background.Height = page.Height

                    If opacity <> 100 Then background.Canvas.Brush.Opacity = opacity

                    background.Canvas.Brush.Color = color
                    background.Canvas.DrawRectangle(New PdfRectangle(0, 0, page.Width, page.Height), PdfDrawMode.Fill)

                    first = False
                End If

                page.Canvas.DrawXObject(background, 0, 0)
            Next
        End Sub
    End Class
End Namespace
