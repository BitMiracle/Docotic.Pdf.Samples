Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class FindControlByName
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const pathToFile As String = "FindControlByName.pdf"

            Using pdf As New PdfDocument("Sample Data\form.pdf")
                Dim emailTextBox As PdfTextBox = TryCast(pdf.GetControl("email"), PdfTextBox)
                Debug.Assert(emailTextBox IsNot Nothing)

                emailTextBox.Text = "support@bitmiracle.com"

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace