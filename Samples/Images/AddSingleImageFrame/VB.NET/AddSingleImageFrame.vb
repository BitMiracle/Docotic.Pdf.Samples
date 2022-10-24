Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddSingleImageFrame
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "AddSingleImageFrame.pdf"

            Using pdf As New PdfDocument()
                Dim frames As PdfImageFrames = pdf.OpenImage("..\Sample Data\multipage.tif")
                Dim secondFrame As PdfImageFrame = frames(1)

                Dim image As PdfImage = pdf.AddImage(secondFrame)
                pdf.Pages(0).Canvas.DrawImage(image, 10, 10, 0)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace