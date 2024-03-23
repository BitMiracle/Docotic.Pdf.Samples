Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class TextFieldAlignment
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Using pdf As New PdfDocument()
                Dim startPoint As New PdfPoint(10, 10)
                Dim size As New PdfSize(100, 100)
                Dim distance As Single = 30.0F
                Dim horizontalAlignments As PdfTextAlign() = {PdfTextAlign.Left, PdfTextAlign.Center, PdfTextAlign.Right}

                Dim page As PdfPage = pdf.Pages(0)

                For h As Integer = 0 To horizontalAlignments.Length - 1
                    Dim left As Single = startPoint.X + h * (size.Width + distance)
                    Dim top As Single = startPoint.Y
                    Dim textBox As PdfTextBox = page.AddTextBox(left, top, size.Width, size.Height)

                    textBox.Multiline = True
                    textBox.TextAlign = horizontalAlignments(h)
                    textBox.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum eu ligula ligula, sit amet tempor odio."
                Next

                Dim pathToFile As String = "TextFieldAlignment.pdf"
                pdf.Save(pathToFile)

                Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

                Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
            End Using
        End Sub
    End Class
End Namespace