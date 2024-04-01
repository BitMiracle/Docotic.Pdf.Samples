Imports System.Text

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Samples
    Public NotInheritable Class TextFromLink
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Using pdf As New PdfDocument("..\Sample Data\Link.pdf")
                Dim linkInfo As LinkInfo = GetLinks(pdf).FirstOrDefault()
                If linkInfo Is Nothing Then
                    Console.WriteLine("Document doesn't contain links!")
                    Return
                End If

                Dim linkDescription As New StringBuilder()
                linkDescription.AppendLine("Number of page with the link: " + linkInfo.OwnerPageIndex.ToString())
                linkDescription.AppendLine("Link's index in document widgets collection: " + linkInfo.OwnerPageWidgetIndex.ToString())
                linkDescription.AppendLine("Link bounds: " + linkInfo.Bounds.ToString())
                linkDescription.AppendLine("Link points to page # " + pdf.IndexOf(linkInfo.TargetPage).ToString())
                linkDescription.AppendLine("NOTE: All page numbers are zero-based. If you use trial version of Docotic.Pdf then even pages of original document are not loaded.")

                linkDescription.AppendLine()
                linkDescription.AppendLine("Text from link:")
                linkDescription.AppendLine(GetTextFromLink(linkInfo.TargetPage, linkInfo.TopOffset))

                Console.WriteLine(linkDescription.ToString())
            End Using
        End Sub

        Private Shared Iterator Function GetLinks(pdf As PdfDocument) As IEnumerable(Of LinkInfo)
            For i As Integer = 0 To pdf.PageCount - 1
                Dim page As PdfPage = pdf.Pages(i)
                Dim widgetIndex As Integer = 0

                For Each widget As PdfWidget In page.Widgets
                    Dim actionArea As PdfActionArea = TryCast(widget, PdfActionArea)

                    If actionArea IsNot Nothing Then
                        Dim linkAction As PdfGoToAction = TryCast(actionArea.Action, PdfGoToAction)

                        If linkAction IsNot Nothing Then
                            Dim view As PdfDocumentView = linkAction.View

                            If view IsNot Nothing Then
                                ' lets ignore links which point to an absent page
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

            ' small reserve for text start vertical position
            Const eps As Single = 5.0F

            Dim textFromTargetPage As PdfCollection(Of PdfTextData) = targetPage.Canvas.GetTextData()
            For Each textData As PdfTextData In textFromTargetPage
                If textData.Position.Y < targetPage.Height - topOffset - eps Then
                    Continue For
                End If

                result.Append(textData.GetText() + " ")
            Next

            Return result.ToString()
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