Imports System.Text
Imports BitMiracle.Docotic

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class GetAllAttachments
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

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