Imports System.IO
Imports System.Windows.Forms

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExportFdfData
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim fdfFile As String = "ExportFdfData.fdf"

            Using pdf As New PdfDocument("Sample Data/form.pdf")
                For Each control As PdfControl In pdf.GetControls
                    Select Case control.Name
                        Case "login"
                            DirectCast(control, PdfTextBox).Text = "Some Name"
                            Exit Select

                        Case "email"
                            DirectCast(control, PdfTextBox).Text = "email@gmail.com"
                            Exit Select

                        Case "user_viewemail"
                            DirectCast(control, PdfCheckBox).Checked = False
                            Exit Select

                        Case "user_sorttopics"
                            ' Check the radio button "Show new topics first"
                            Dim sortTopics As PdfRadioButton = DirectCast(control, PdfRadioButton)
                            If sortTopics.ExportValue = "user_not_sorttopics" Then
                                sortTopics.Checked = True
                            End If

                            Exit Select
                    End Select
                Next

                pdf.ExportFdf(fdfFile)
            End Using

            MessageBox.Show("FDF file is exported to " + Path.Combine(Directory.GetCurrentDirectory(), fdfFile))
        End Sub
    End Class
End Namespace