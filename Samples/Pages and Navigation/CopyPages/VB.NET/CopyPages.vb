Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CopyPages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "CopyPages.pdf"

            Using pdf As New PdfDocument("..\Sample Data\jfif3.pdf")

                ' copy third and first pages to a new PDF document (page indexes are zero-based)
                Using copy As PdfDocument = pdf.CopyPages(New Integer() {2, 0})
                    ' Helps to reduce file size in cases when the copied pages reference
                    ' unused resources such as fonts, images, patterns.
                    copy.RemoveUnusedResources()

                copy.Save(pathToFile)
            End Using

            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace