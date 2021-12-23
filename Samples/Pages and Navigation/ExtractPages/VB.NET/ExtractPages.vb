Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExtractPages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using original As New PdfDocument()
                For i As Integer = 0 To 4
                    If i <> 0 Then
                        original.AddPage()
                    End If

                    Dim page As PdfPage = original.Pages(i)
                    page.Canvas.DrawString("Page #" + (i + 1).ToString())
                Next

                ' extract first 3 pages. These pages will be removed from original PDF document.
                Using extracted As PdfDocument = original.ExtractPages(0, 3)
                    ' Helps to reduce file size in cases when the extracted pages reference
                    ' unused resources such as fonts, images, patterns.
                    extracted.RemoveUnusedResources()

                    extracted.Save("ExtractPages.pdf")
                End Using

                original.Save("ExtractPages_original.pdf")
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace