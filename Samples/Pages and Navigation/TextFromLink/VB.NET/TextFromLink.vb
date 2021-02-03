Imports System
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Samples
    Public NotInheritable Class TextFromLink
        Private Class LinkInfo
            Public ReadOnly Action As PdfGoToAction
            Public ReadOnly Index As Integer = -1
            Public ReadOnly Bounds As PdfRectangle
            Public ReadOnly OwnerPageNumber As Integer = -1
            Public ReadOnly TargetPageNumber As Integer = -1

            Public Sub New(ByVal pdf As PdfDocument, ByVal actionArea As PdfActionArea, ByVal linkIndex As Integer)
                If pdf Is Nothing Then
                    Throw New ArgumentNullException("document")
                End If

                If actionArea Is Nothing Then
                    Throw New ArgumentNullException("actionArea")
                End If

                Action = TryCast(actionArea.Action, PdfGoToAction)
                If Action Is Nothing Then
                    Throw New ArgumentException("Action area doesn't contain link", "actionArea")
                End If

                Index = linkIndex
                Bounds = actionArea.BoundingBox
                OwnerPageNumber = pdf.IndexOf(actionArea.Owner)
                TargetPageNumber = pdf.IndexOf(Action.View.Page)
            End Sub
        End Class

        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument("Sample Data\Link.pdf")
                Dim linkInfo As LinkInfo = getFirstLink(pdf)
                If linkInfo Is Nothing Then
                    MessageBox.Show("Document doesn't contain links!")
                    Return
                End If

                Dim linkDescription As New StringBuilder()
                linkDescription.AppendLine("Link's index in document widgets collection: " + linkInfo.Index.ToString())
                linkDescription.AppendLine("Number of page with link: " + linkInfo.OwnerPageNumber.ToString())
                linkDescription.AppendLine("Link bounds: " + linkInfo.Bounds.ToString())
                linkDescription.AppendLine("Link points to page # " + linkInfo.TargetPageNumber.ToString())
                linkDescription.AppendLine("NOTE: All page numbers are zero-based. If you use trial version of Docotic.Pdf then even pages of original document are not loaded.")

                linkDescription.AppendLine()
                linkDescription.AppendLine("Text from link:")
                linkDescription.AppendLine(getTextFromLink(linkInfo.Action))

                System.Diagnostics.Process.Start("Sample Data\Link.pdf")
                MessageBox.Show(linkDescription.ToString())
            End Using
        End Sub

        Private Shared Function getFirstLink(pdf As PdfDocument) As LinkInfo
            Dim i As Integer = 0
            For Each widget As PdfWidget In pdf.GetWidgets()
                Dim actionArea As PdfActionArea = TryCast(widget, PdfActionArea)
                If actionArea IsNot Nothing Then
                    Dim linkAction As PdfGoToAction = TryCast(actionArea.Action, PdfGoToAction)
                    If linkAction IsNot Nothing Then
                        ' lets ignore links which point to an absent page
                        If linkAction.View.Page IsNot Nothing Then
                            Return New LinkInfo(pdf, actionArea, i)
                        End If
                    End If
                End If

                i += 1
            Next

            Return Nothing
        End Function

        Private Shared Function getTextFromLink(ByVal linkAction As PdfGoToAction) As String
            Dim targetPage As PdfPage = linkAction.View.Page
            If targetPage Is Nothing Then
                Return [String].Empty
            End If

            Dim result As New StringBuilder()

            Const eps As Single = 5.0F
            ' small reserve for text start vertical position
            Dim textFromTargetPage As PdfCollection(Of PdfTextData) = targetPage.Canvas.GetTextData()
            For Each textData As PdfTextData In textFromTargetPage
                If textData.Position.Y < targetPage.Height - linkAction.View.Top - eps Then
                    Continue For
                End If

                result.Append(textData.GetText() + " ")
            Next

            Return result.ToString()
        End Function
    End Class
End Namespace