Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class EditPageContent
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "EditPageContent.pdf"

            Using pdf = New PdfDocument("..\Sample Data\form.pdf")
                For Each page As PdfPage In pdf.Pages
                    Dim editor = New PageContentEditor(pdf, Nothing, AddressOf ReplaceColor, AddressOf ShouldRemoveText)
                    editor.Edit(page)
                Next

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        ''' <summary>
        ''' Replaces orange color with gray.
        ''' </summary>
        Private Shared Function ReplaceColor(color As PdfColor) As PdfColor
            Dim rgb As PdfRgbColor = TryCast(color, PdfRgbColor)
            If rgb IsNot Nothing AndAlso rgb.R = 255 AndAlso rgb.G = 127 AndAlso rgb.B = 64 Then
                Return New PdfGrayColor(70)
            End If

            Return color
        End Function

        ''' <summary>
        ''' Removes "(Max...)" text on the right.
        ''' </summary>
        Private Shared Function ShouldRemoveText(text As PdfTextData) As Boolean
            Return text.Bounds.X > 200
        End Function
    End Class
End Namespace