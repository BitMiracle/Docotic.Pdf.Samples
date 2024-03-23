Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RemoveXObjectsWatermarks
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "RemoveXObjectsWatermarks.pdf"

            Using pdf As New PdfDocument("..\Sample Data\DocumentWithWatermark.pdf")
                For Each page As PdfPage In pdf.Pages
                    ' remove first XObject (in this case it's a watermark)
                    page.XObjects.RemoveAt(0)
                Next

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
