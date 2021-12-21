Imports System.IO
Imports System.Reflection

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SavePageAsImage
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToImage As String = "SavePageAsImage.jpg"

            Dim location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            Using pdf As New PdfDocument(Path.Combine(location, "jfif3.pdf"))
                Dim options As PdfDrawOptions = PdfDrawOptions.Create()
                options.BackgroundColor = New PdfRgbColor(255, 255, 255)
                options.Compression = ImageCompressionOptions.CreateJpeg()

                pdf.Pages(1).Save(pathToImage, options)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace