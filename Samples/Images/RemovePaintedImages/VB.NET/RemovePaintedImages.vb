Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RemovePaintedImages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const PathToFile As String = "RemovePaintedImages.pdf"

            Using pdf = New PdfDocument("..\Sample Data\ImageScaleAndRotate.pdf")
                Dim page As PdfPage = pdf.Pages(0)
                page.RemovePaintedImages(
                    Function(image)
                        ' remove rotated images
                        Dim m As PdfMatrix = image.TransformationMatrix
                        Return Math.Abs(m.M12) > 0.001 Or Math.Abs(m.M21) > 0.001
                    End Function
                )
                pdf.Save(PathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace