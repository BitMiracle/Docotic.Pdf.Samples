Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class NamedDestinations
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "NamedDestinations.pdf"

            Using pdf As New PdfDocument("..\Sample Data\links-with-named-destinations.pdf")

                ' Read the existing named views (destinations).
                For Each entry In pdf.SharedViews
                    Console.WriteLine($"{entry.Name} => {ViewToString(entry.View, pdf)}")
                Next

                ' Add another named view.
                Dim ViewName As String = "Test destination name"
                Dim TargetPageIndex As Integer = 2
                Dim view As PdfDocumentView = pdf.CreateView(TargetPageIndex)
                view.SetZoom(New PdfPoint(0, 300), 0)
                pdf.SharedViews.Add(ViewName, view)

                ' Use the newly created named view in a go-to action.
                Dim gotoAction As PdfGoToAction = pdf.CreateGoToPageAction(ViewName)
                Dim area As PdfActionArea = pdf.Pages(0).AddActionArea(200, 50, 30, 20, gotoAction)
                area.Border.Color = New PdfRgbColor(255, 0, 0)
                area.Border.Width = 2

                ' Add some text at the target position.
                pdf.Pages(TargetPageIndex).Canvas.DrawString(0, 300, "You have reached the new named destination")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Function ViewToString(view As PdfDocumentView, pdf As PdfDocument) As String
            If view Is Nothing Then Return "undefined"

            If view.Page IsNot Nothing Then Return $"page index {FindPageIndex(pdf, view.Page)}"

            Return $"page index {view.PageIndex}"
        End Function

        Private Shared Function FindPageIndex(pdf As PdfDocument, page As PdfPage) As Integer
            Dim pageToFindDictString = page.Dictionary.ToString()
            If pageToFindDictString IsNot Nothing Then
                For i As Integer = 0 To pdf.Pages.Count - 1
                    Dim pageDictString = pdf.Pages(i).Dictionary.ToString()
                    If pageToFindDictString = pageDictString Then
                        Return i
                    End If
                Next
            End If

            Return -1
        End Function
    End Class
End Namespace