Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SaveAttachment
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument("..\Sample Data\Attachments.pdf")
                Dim spec As PdfFileSpecification = pdf.SharedAttachments("File Attachment testing.doc")
                If spec IsNot Nothing And spec.Contents IsNot Nothing Then
                    Const pathToFile As String = "attachment.doc"
                    spec.Contents.Save(pathToFile)
                    Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
                Else
                    Console.WriteLine($"Can't save the shared attachment")
                End If
            End Using
        End Sub
    End Class
End Namespace