Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SaveAsBitonalTiff
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim outputDocumentPath As String = "SaveAsBitonalTiff.tiff"
            Dim outputPagePath As String = "SaveAsBitonalTiff_page0.tiff"

            Using pdf As New PdfDocument("..\Sample Data\jfif3.pdf")
                Dim options As PdfDrawOptions = PdfDrawOptions.Create()
                options.BackgroundColor = New PdfRgbColor(255, 255, 255)
                options.HorizontalResolution = 300
                options.VerticalResolution = 300

                ' specify bitonal TIFF as the desired output compression
                options.Compression = ImageCompressionOptions.CreateTiff().
                    SetBitonal()

                ' save one page
                pdf.Pages(0).Save(outputPagePath, options)

                ' save the whole document as multipage TIFF
                pdf.SaveAsTiff(outputDocumentPath, options)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(outputPagePath) With {.UseShellExecute = True})
            Process.Start(New ProcessStartInfo(outputDocumentPath) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
