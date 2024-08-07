Imports System.IO
Imports BitMiracle.Docotic

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExportFdfData
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim fdfFile As String = "ExportFdfData.fdf"

            Using pdf As New PdfDocument("..\Sample Data\form.pdf")
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

            Console.WriteLine("FDF file is exported to " + Path.Combine(Directory.GetCurrentDirectory(), fdfFile))
        End Sub
    End Class
End Namespace