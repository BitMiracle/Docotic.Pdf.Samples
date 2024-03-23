Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExtractText
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim documentTextFile As String = "Document text.txt"
            Dim documentTextFormattedFile As String = "Document text with formatting.txt"
            Dim firstPageTextFile As String = "First page text.txt"

            Using pdf As New PdfDocument("..\Sample data\jfif3.pdf")

                ' Extract plain text from document
                Using writer As New StreamWriter(documentTextFile)
                    writer.Write(pdf.GetText())
                End Using

                ' Extract text with formatting from document
                Using writer As New StreamWriter(documentTextFormattedFile)
                    writer.Write(pdf.GetTextWithFormatting())
                End Using

                ' Only extract visible plain text from first page
                Using writer As New StreamWriter(firstPageTextFile)
                    Dim options = New PdfTextExtractionOptions()
                    options.WithFormatting = False
                    options.SkipInvisibleText = True

                    writer.Write(pdf.Pages(0).GetText(options))
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(documentTextFile) With {.UseShellExecute = True})
            Process.Start(New ProcessStartInfo(documentTextFormattedFile) With {.UseShellExecute = True})
            Process.Start(New ProcessStartInfo(firstPageTextFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace