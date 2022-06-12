Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class EditPageContent
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf = New PdfDocument("..\Sample Data\form.pdf")
                Dim pageCount As Integer = pdf.PageCount
                For i As Integer = 0 To pageCount - 1
                    Dim sourcePage As PdfPage = pdf.Pages(0)
                    Dim tempPage As PdfPage = pdf.AddPage()

                    ' copy And modify page objects to a temporary page
                    Dim copier = New PageObjectCopier(pdf, Nothing, AddressOf replaceColor, AddressOf shouldRemoveText)
                    copier.Copy(sourcePage, tempPage)

                    ' clear content of the source page
                    sourcePage.Canvas.Clear()

                    ' copy modified objects from the temporary page to the source page
                    tempPage.Rotation = PdfRotation.None
                    Dim xobj As PdfXObject = pdf.CreateXObject(tempPage)
                    Dim mediaBox As PdfBox = sourcePage.MediaBox
                    sourcePage.Canvas.DrawXObject(xobj, mediaBox.Left, -mediaBox.Bottom, xobj.Width, xobj.Height, 0)

                    ' remove the temporary page
                    pdf.RemovePage(pageCount)
                Next

                pdf.Save("EditPageContent.pdf")
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub

        ''' <summary>
        ''' Replaces orange color with gray.
        ''' </summary>
        Private Shared Function replaceColor(color As PdfColor) As PdfColor
            Dim rgb As PdfRgbColor = TryCast(color, PdfRgbColor)
            If rgb IsNot Nothing AndAlso rgb.R = 255 AndAlso rgb.G = 127 AndAlso rgb.B = 64 Then
                Return New PdfGrayColor(70)
            End If

            Return color
        End Function

        ''' <summary>
        ''' Removes "(Max...)" text on the right.
        ''' </summary>
        Private Shared Function shouldRemoveText(text As PdfTextData) As Boolean
            Return text.Bounds.X > 200
        End Function
    End Class
End Namespace