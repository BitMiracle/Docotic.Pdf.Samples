Imports System.Text
Imports System.Windows.Forms

Imports Microsoft.VisualBasic

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CheckIfPasswordProtected
        Public Shared Sub Main()
            Dim message As New StringBuilder()

            Dim documentsToCheck As String() = {"encrypted.pdf", "jfif3.pdf"}
            For Each fileName As String In documentsToCheck
                Dim passwordProtected As Boolean = PdfDocument.IsPasswordProtected("Sample Data\" + fileName)
                If passwordProtected Then
                    message.AppendFormat("{0} - REQUIRES PASSWORD" & vbCr & vbLf, fileName)
                Else
                    message.AppendFormat("{0} - DOESN'T REQUIRE PASSWORD" & vbCr & vbLf, fileName)
                End If
            Next

            MessageBox.Show(message.ToString())
        End Sub
    End Class
End Namespace