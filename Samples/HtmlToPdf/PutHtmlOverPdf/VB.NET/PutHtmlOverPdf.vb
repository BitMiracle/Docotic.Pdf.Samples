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
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "PutHtmlOverPdf.pdf"

            Using converter = Await HtmlConverter.CreateAsync()
                ' Specify the page size. It should usually match the size of the page that you
                ' want to overlay.
                Dim options = New HtmlConversionOptions()
                options.Page.SetSizeInches(4.13, 5.83)

                ' The library uses a transparent background by default. To make the background
                ' opaque, you can set it using CSS, for example
                ' options.Start.SetStartAfterScriptRun("document.body.style.background = 'white'")
                '
                '  Alternatively, you can add a white background watermark to every converted page.
                ' See the addWhiteBackground method below for an example.

                Dim htmlCode = "<div style=""position: absolute; top: 270px; right: 100px;\"">" +
                    "I would like to put this here</div>"
                Using htmlPdf = Await converter.CreatePdfFromStringAsync(htmlCode, options)
                    ' Uncomment to add a semi-transparent red background:
                    'AddBackground(htmlPdf, New PdfRgbColor(255, 0, 0), 25)

                    Using pdf = New PdfDocument("..\Sample Data\simple-graphics.pdf")
                        ' Create an XObject from the first page of the HTML-generated PDF.
                        ' Note that the page And the method are accessed through different
                        ' PdfDocument instances.
                        Dim xObj = pdf.CreateXObject(htmlPdf.Pages(0))

                        ' Draw the XObject on a page of the existing PDF document.
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
