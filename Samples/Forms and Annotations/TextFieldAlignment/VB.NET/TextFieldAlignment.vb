Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class TextFieldAlignment
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

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