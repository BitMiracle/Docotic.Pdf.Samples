Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class LinkToPage
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "LinkToPage.pdf"

            Using pdf As New PdfDocument()
                pdf.AddPage()
                pdf.AddPage()

                Dim firstPage As PdfPage = pdf.Pages(0)
                Dim canvas As PdfCanvas = firstPage.Canvas
                Dim rectForLinkToSecondPage As New PdfRectangle(10, 50, 100, 60)
                canvas.DrawRectangle(rectForLinkToSecondPage, PdfDrawMode.Stroke)
                canvas.DrawString("Go to 2nd page", rectForLinkToSecondPage, PdfTextAlign.Center, PdfVerticalAlign.Center)
                firstPage.AddLinkToPage(rectForLinkToSecondPage, 1)

                Dim rectForLinkToThirdPage As New PdfRectangle(150, 50, 100, 60)
                canvas.DrawRectangle(rectForLinkToThirdPage, PdfDrawMode.Stroke)
                canvas.DrawString("Go to 3rd page", rectForLinkToThirdPage, PdfTextAlign.Center, PdfVerticalAlign.Center)
                firstPage.AddLinkToPage(rectForLinkToThirdPage, pdf.Pages(2))

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace