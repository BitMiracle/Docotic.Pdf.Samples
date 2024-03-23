Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class FillForm
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "FillForm.pdf"

            Using pdf As New PdfDocument("..\Sample data\form.pdf")
                For Each control As PdfControl In pdf.GetControls
                    Select Case control.Name
                        Case "login"
                            DirectCast(control, PdfTextBox).Text = "Some Name"
                            Exit Select

                        Case "passwd", "passwd2"
                            Dim password As PdfTextBox = DirectCast(control, PdfTextBox)
                            password.Text = "Password"
                            password.PasswordField = True
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

                        Case "BtnRegister"
                            DirectCast(control, PdfButton).[ReadOnly] = True
                            Exit Select
                    End Select
                Next

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace