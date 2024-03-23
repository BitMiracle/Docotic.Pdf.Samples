Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class TextMarkupAnnotations
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "TextMarkupAnnotations.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)
                Dim canvas As PdfCanvas = page.Canvas

                ' Draw text on the page
                Const Text As String = "Highlighted text"
                Dim textPosition As New PdfPoint(10, 50)
                canvas.FontSize = 30
                canvas.DrawString(textPosition, Text)

                ' Get size of the drawn text
                Dim size As PdfSize = canvas.MeasureText(Text)
                Dim bounds = New PdfRectangle(textPosition, size)

                ' Highlight and annotate the text
                Dim color = New PdfRgbColor(0, 0, 255)
                Const AnnotationContents As String = "Lorem ipsum"
                page.AddHighlightAnnotation(AnnotationContents, bounds, color)

                ' Or call these method to strike out or underline the text:
                ' page.AddStrikeoutAnnotation(AnnotationContents, bounds, color);
                ' page.AddJaggedUnderlineAnnotation(AnnotationContents, bounds, color);
                ' page.AddUnderlineAnnotation(AnnotationContents, bounds, color);

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace