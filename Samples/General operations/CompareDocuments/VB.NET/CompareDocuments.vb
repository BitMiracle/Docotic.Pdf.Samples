Imports System.Text

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CompareDocuments
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const originalFile As String = "..\Sample Data\Link.pdf"
            Using first As New PdfDocument(originalFile)
                first.Save("first.pdf")
            End Using

            Using second As New PdfDocument(originalFile)
                second.Save("second.pdf")
            End Using

            Using third As New PdfDocument(originalFile)
                third.Pages(0).Canvas.DrawString("Hello")
                third.Save("third.pdf")
            End Using

            Dim message As New StringBuilder()

            message.AppendLine("first.pdf equals to second.pdf?")
            If PdfDocument.DocumentsAreEqual("first.pdf", "second.pdf") Then
                message.AppendLine("Yes")
            Else
                message.AppendLine("No")
            End If

            message.AppendLine()

            message.AppendLine("first.pdf equals to third.pdf?")
            If PdfDocument.DocumentsAreEqual("first.pdf", "third.pdf") Then
                message.AppendLine("Yes")
            Else
                message.AppendLine("No")
            End If

            Console.WriteLine(message.ToString())
        End Sub
    End Class
End Namespace