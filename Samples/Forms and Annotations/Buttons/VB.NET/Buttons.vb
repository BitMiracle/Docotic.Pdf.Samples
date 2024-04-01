Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Buttons
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "Buttons.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Dim button As PdfButton = page.AddButton(10, 50, 100, 50)
                button.BackgroundColor = New PdfGrayColor(60)
                button.Border.Color = New PdfRgbColor(128, 0, 128)
                button.Text = "Button"

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace