Imports System.IO
Imports System.Reflection
Imports System.Text

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class GetAllAttachments
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            Using pdf As New PdfDocument(Path.Combine(location, "Attachments.pdf"))
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
                        attachmentNames.AppendLine(fileAnnot.File.Specification)
                    Next
                Next

                Console.WriteLine("Attachments List:")
                Console.WriteLine(attachmentNames.ToString())
            End Using
        End Sub
    End Class
End Namespace