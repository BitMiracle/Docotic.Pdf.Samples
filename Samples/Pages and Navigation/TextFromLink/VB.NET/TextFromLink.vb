Imports System.Text
Imports BitMiracle.Docotic

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Samples
    Public NotInheritable Class TextFromLink
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Using pdf As New PdfDocument("..\Sample Data\Link.pdf")
                Dim linkInfo As LinkInfo = GetLinks(pdf).FirstOrDefault()
                If linkInfo Is Nothing Then
                    Console.WriteLine("Document doesn't contain links!")
                    Return
                End If

                Dim linkDescription As New StringBuilder()
                linkDescription.AppendLine("Link is at page index: " + linkInfo.OwnerPageIndex.ToString())
                linkDescription.AppendLine("Link index in the page's widgets collection: " + linkInfo.OwnerPageWidgetIndex.ToString())
                linkDescription.AppendLine("Link bounds: " + linkInfo.Bounds.ToString())
                linkDescription.AppendLine("Target page index: " + FindPageIndex(pdf, linkInfo.TargetPage).ToString())
                linkDescription.AppendLine("Note: All page indexes are zero-based.")

                linkDescription.AppendLine()
                linkDescription.AppendLine("Text from link:")
                linkDescription.AppendLine(GetTextFromLink(linkInfo.TargetPage, linkInfo.TopOffset))

                Console.WriteLine(linkDescription.ToString())
            End Using
        End Sub

        Private Shared Iterator Function GetLinks(pdf As PdfDocument) As IEnumerable(Of LinkInfo)
            For i As Integer = 0 To pdf.Pages.Count - 1
                Dim page As PdfPage = pdf.Pages(i)
                Dim widgetIndex As Integer = 0

                For Each widget As PdfWidget In page.Widgets
                    Dim actionArea As PdfActionArea = TryCast(widget, PdfActionArea)

                    If actionArea IsNot Nothing Then
                        Dim linkAction As PdfGoToAction = TryCast(actionArea.Action, PdfGoToAction)

                        If linkAction IsNot Nothing Then
                            Dim view As PdfDocumentView = linkAction.GetView().View

                            If view IsNot Nothing Then
                                ' Skip links that point to a nonexistent page.
                                Dim targetPage As PdfPage = view.Page
                                If targetPage IsNot Nothing Then
                                    Yield New LinkInfo(i, widgetIndex, actionArea.BoundingBox, targetPage, view.Top)
                                End If
                            End If
                        End If
                    End If

                    widgetIndex += 1
                Next
            Next
        End Function

        Private Shared Function GetTextFromLink(targetPage As PdfPage, topOffset As Double?) As String
            Dim result As New StringBuilder()

            ' Allow some extra tolerance in the vertical position of the text start.
            Const eps As Single = 5.0F

            Dim textFromTargetPage = targetPage.Canvas.GetTextData()
            For Each textData As PdfTextData In textFromTargetPage
                If textData.Position.Y < targetPage.Height - topOffset - eps Then
                    Continue For
                End If

                result.Append(textData.GetText() + " ")
            Next

            Return result.ToString()
        End Function

        Private Shared Function FindPageIndex(pdf As PdfDocument, page As PdfPage) As Integer
            Dim pageToFindDictString = page?.Dictionary.ToString()
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

        Private Class LinkInfo
            Public ReadOnly OwnerPageIndex As Integer
            Public ReadOnly OwnerPageWidgetIndex As Integer
            Public ReadOnly Bounds As PdfRectangle
            Public ReadOnly TargetPage As PdfPage
            Public ReadOnly TopOffset As Double?

            Public Sub New(
                linkPageIndex As Integer,
                linkPageWidgetIndex As Integer,
                linkBounds As PdfRectangle,
                page As PdfPage,
                offset As Double?)

                OwnerPageIndex = linkPageIndex
                OwnerPageWidgetIndex = linkPageWidgetIndex
                Bounds = linkBounds
                TargetPage = page
                TopOffset = offset

            End Sub
        End Class
    End Class
End Namespace