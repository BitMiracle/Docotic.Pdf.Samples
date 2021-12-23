Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ReplaceImage
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf = New PdfDocument("..\Sample Data\gmail-cheat-sheet.pdf")
                For Each image In pdf.GetImages(False)
                    image.ReplaceWith("..\Sample Data\ammerland.jpg")
                Next

                pdf.Save("ReplaceImage.pdf")
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace