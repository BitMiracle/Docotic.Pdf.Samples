Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Outline
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "Outline.pdf"

            Using pdf As New PdfDocument()
                pdf.PageMode = PdfPageMode.UseOutlines

                pdf.AddPage()
                pdf.AddPage()

                For i As Integer = 0 To pdf.PageCount - 1
                    Dim canvas As PdfCanvas = pdf.Pages(i).Canvas
                    canvas.DrawString(260, 50, "Page " + (i + 1).ToString())
                Next

                Dim root As PdfOutlineItem = pdf.OutlineRoot
                root.AddChild("Page 1", 0)
                Dim outlineForPage2 As PdfOutlineItem = root.AddChild("Page 2", 1)
                outlineForPage2.AddChild("Page 3", 2)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace