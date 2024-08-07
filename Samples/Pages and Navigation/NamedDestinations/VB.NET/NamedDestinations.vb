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

                ' Read existing named views (destinations)
                For Each kvp As KeyValuePair(Of String, PdfDocumentView) In pdf.SharedViews
                    Console.WriteLine($"{kvp.Key} => {viewToString(kvp.Value, pdf)}")
                Next

                ' Add another named view
                Dim ViewName As String = "Test destination name"
                Dim TargetPageIndex As Integer = 2
                Dim view As PdfDocumentView = pdf.CreateView(TargetPageIndex)
                view.SetZoom(New PdfPoint(0, 300), 0)
                pdf.SharedViews.Add(ViewName, view)

                ' Use the created named view in a go-to action
                Dim gotoAction As PdfGoToAction = pdf.CreateGoToPageAction(ViewName)
                Dim area As PdfActionArea = pdf.Pages(0).AddActionArea(200, 50, 30, 20, gotoAction)
                area.Border.Color = New PdfRgbColor(255, 0, 0)
                area.Border.Width = 2

                ' Add some text to the target position
                pdf.Pages(TargetPageIndex).Canvas.DrawString(0, 300, "From the new named destination")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Function viewToString(view As PdfDocumentView, pdf As PdfDocument) As String
            If view.Page IsNot Nothing Then Return $"page index {pdf.Pages.IndexOf(view.Page)}"

            Return $"page index {view.PageIndex}"
        End Function
    End Class
End Namespace