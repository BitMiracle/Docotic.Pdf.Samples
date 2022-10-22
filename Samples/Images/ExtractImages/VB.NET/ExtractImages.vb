Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExtractImages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = ""

            Using pdf As New PdfDocument("..\Sample Data\gmail-cheat-sheet.pdf")
                For Each image As PdfImage In pdf.GetImages()
                    pathToFile = image.Save("ExtractedImage")

                    ' Only extract first image in this sample
                    Exit For
                Next
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace