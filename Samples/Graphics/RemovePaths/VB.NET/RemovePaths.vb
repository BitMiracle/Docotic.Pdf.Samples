Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RemovePaths
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const PathToFile As String = "RemovePaths.pdf"

            Using pdf = New PdfDocument("..\Sample Data\form.pdf")
                Dim page As PdfPage = pdf.Pages(0)
                page.RemovePaths(
                    Function(path)
                        ' remove the form header filled with orange (255, 127, 64)
                        If path.PaintMode = PdfDrawMode.Stroke Then Return False

                        Dim fillColor As PdfRgbColor = TryCast(path.Brush.Color, PdfRgbColor)
                        Return (
                            fillColor IsNot Nothing AndAlso
                            fillColor.R = 255 AndAlso
                            fillColor.G = 127 AndAlso
                            fillColor.B = 64
                        )
                    End Function
                )
                pdf.Save(PathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace