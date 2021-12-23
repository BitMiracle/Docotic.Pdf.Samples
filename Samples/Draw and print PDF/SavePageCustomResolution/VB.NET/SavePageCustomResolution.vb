Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SavePageCustomResolution
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToImage As String = "SavePageCustomResolution.png"

            Using pdf As New PdfDocument("..\Sample Data\gmail-cheat-sheet.pdf")
                Dim options As PdfDrawOptions = PdfDrawOptions.Create()
                options.BackgroundColor = New PdfRgbColor(255, 255, 255)
                options.HorizontalResolution = 600
                options.VerticalResolution = 600

                pdf.Pages(0).Save(pathToImage, options)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace