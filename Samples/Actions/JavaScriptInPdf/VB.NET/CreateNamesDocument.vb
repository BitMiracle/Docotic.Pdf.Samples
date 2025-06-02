Imports System.IO
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class CreateNamesDocument
        Private Const LeftMargin As Double = 80 ' the width of the left margin of the document
        Private Const TopMargin As Double = 50 ' the height of the top margin of the document
        Private Const RightMargin As Double = 50 ' the width of the right margin of the document

        Private Const FontSize As Double = 10 ' font size in the document
        Private Const LoremIpsumFontSize As Double = 7 ' font size of the lorem ipsum part

        Public Shared Sub Run()
            Dim pathToFile As String = "CreateNamesDocument.pdf"
            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Dim canvas As PdfCanvas = page.Canvas
                canvas.FontSize = FontSize

                DrawNameField(canvas, 40, page, 0)
                DrawLoremIpsum(canvas, page.Width)
                DrawNameField(canvas, 200, page, 1)

                pdf.Save(pathToFile)
            End Using

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub DrawNameField(canvas As PdfCanvas, offsetFromTopMargin As Double, page As PdfPage, nameFieldIndex As Integer)
            Dim caption As String = "First name:"

            Dim left As Double = LeftMargin
            Dim top As Double = TopMargin + offsetFromTopMargin
            canvas.DrawString(left, top, caption)

            Dim captionSize As PdfSize = canvas.MeasureText(caption)
            Dim width As Double = page.Width - RightMargin - LeftMargin - captionSize.Width

            Dim textBox As PdfTextBox = page.AddTextBox(
                "name" & nameFieldIndex.ToString(),
                canvas.TextPosition.X,
                canvas.TextPosition.Y,
                width,
                captionSize.Height)
            textBox.Font = canvas.Font
            textBox.FontSize = FontSize
            textBox.Border.Width = 0
            textBox.TextAlign = PdfTextAlign.Center
        End Sub

        Private Shared Sub DrawLoremIpsum(canvas As PdfCanvas, pageWidth As Double)
            Dim fontSizeBefore As Double = FontSize
            Try
                Dim loremIpsumBounds As New PdfRectangle(
                    LeftMargin,
                    TopMargin + 60,
                    pageWidth - RightMargin - LeftMargin,
                    150)
                canvas.FontSize = LoremIpsumFontSize

                Dim loremIpsum As String = String.Join(
                    Environment.NewLine, File.ReadAllLines("../Sample Data/lorem-ipsum.txt").Take(5))

                Dim options As New PdfTextDrawingOptions(loremIpsumBounds) With {
                    .HorizontalAlignment = PdfTextAlign.Left,
                    .VerticalAlignment = PdfVerticalAlign.Top
                }
                canvas.DrawText(loremIpsum, options)
            Finally
                canvas.FontSize = fontSizeBefore
            End Try
        End Sub
    End Class
End Namespace
