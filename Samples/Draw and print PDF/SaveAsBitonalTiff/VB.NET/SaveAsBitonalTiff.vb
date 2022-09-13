Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SaveAsBitonalTiff
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

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
        End Sub
    End Class
End Namespace
