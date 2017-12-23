Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Permissions
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Permissions.pdf"

            Using pdf As New PdfDocument("Sample data/form.pdf")

                ' an owner password should be set in order to use user access permissions
                pdf.OwnerPassword = "owner"

                pdf.UserPassword = "user"
                pdf.Permissions.Flags = PdfPermissionFlags.None

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace