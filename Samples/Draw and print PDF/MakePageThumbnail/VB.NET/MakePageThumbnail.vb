Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class MakePageThumbnail
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToImage As String = "MakePageThumbnail.png"

            Using pdf As New PdfDocument("..\Sample Data\jfif3.pdf")
                Dim options As PdfDrawOptions = PdfDrawOptions.CreateFitSize(New PdfSize(200, 200), False)
                options.BackgroundColor = New PdfGrayColor(100)
                pdf.Pages(0).Save(pathToImage, options)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToImage) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace