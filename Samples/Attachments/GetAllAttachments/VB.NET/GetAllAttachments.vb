Imports System.Text

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class GetAllAttachments
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Using pdf As New PdfDocument("..\Sample Data\Attachments.pdf")
                Dim attachmentNames As New StringBuilder()

                ' collect names of files attached to the document
                For Each spec As PdfFileSpecification In pdf.SharedAttachments
                    attachmentNames.AppendLine(spec.Specification)
                Next

                ' collect names of files used in file attachment annotations
                For Each page As PdfPage In pdf.Pages
                    For Each widget As PdfWidget In page.Widgets
                        If widget.Type <> PdfWidgetType.FileAttachment Then
                            Continue For
                        End If

                        Dim fileAnnot As PdfFileAttachmentAnnotation = DirectCast(widget, PdfFileAttachmentAnnotation)
                        If fileAnnot.File IsNot Nothing Then
                            attachmentNames.AppendLine(fileAnnot.File.Specification)
                        Else
                            attachmentNames.AppendLine()
                        End If
                    Next
                Next

                Console.WriteLine("Attachments List:")
                Console.WriteLine(attachmentNames.ToString())
            End Using
        End Sub
    End Class
End Namespace