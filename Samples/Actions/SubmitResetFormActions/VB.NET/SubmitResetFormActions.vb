Imports System
Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SubmitResetFormActions
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "SubmitResetFormActions.pdf"

            Using pdf As New PdfDocument()

                Dim page As PdfPage = pdf.Pages(0)
                Dim textBox As PdfTextBox = page.AddTextBox("sample_textbox", 10, 50, 100, 15)
                textBox.Text = "Hello"

                Dim checkBox As PdfCheckBox = page.AddCheckBox("sample_checkbox", 10, 70, 100, 15, "Check me")
                checkBox.Checked = True

                Dim radioButton As PdfRadioButton = page.AddRadioButton("radio", 10, 90, 100, 15, "Select me!")
                radioButton.ExportValue = "radio1"
                radioButton.Checked = True

                Dim radioButton2 As PdfRadioButton = page.AddRadioButton("radio", 10, 110, 100, 15, "No, select me!")
                radioButton2.ExportValue = "radio2"
                radioButton2.Checked = False

                Dim submitButton As PdfButton = page.AddButton(10, 130, 45, 20)
                submitButton.Text = "Submit"

                Dim submitAction As PdfSubmitFormAction = pdf.CreateSubmitFormAction(New Uri("http://bitmiracle.com/login.php"))
                submitAction.SubmitFormat = PdfSubmitFormat.Html
                submitAction.SubmitMethod = PdfSubmitMethod.[Get]
                submitAction.AddControl(textBox)
                submitAction.AddControl(checkBox)
                submitAction.AddControl(radioButton)
                submitAction.AddControl(radioButton2)
                submitButton.OnMouseDown = submitAction

                Dim resetButton As PdfButton = page.AddButton(65, 130, 45, 20)
                resetButton.Text = "Reset"

                Dim resetAction As PdfResetFormAction = pdf.CreateResetFormAction()
                resetAction.AddControl(textBox)
                resetAction.AddControl(checkBox)
                resetAction.AddControl(radioButton)
                resetAction.AddControl(radioButton2)
                resetButton.OnMouseDown = resetAction

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace