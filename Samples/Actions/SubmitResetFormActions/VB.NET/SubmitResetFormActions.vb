Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SubmitResetFormActions
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

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
                submitButton.OnMouseDown = submitAction

                Dim controls = submitAction.Controls
                controls.Add(textBox)
                controls.Add(checkBox)
                controls.Add(radioButton)
                controls.Add(radioButton2)

                Dim resetButton As PdfButton = page.AddButton(65, 130, 45, 20)
                resetButton.Text = "Reset"

                Dim resetAction As PdfResetFormAction = pdf.CreateResetFormAction()
                resetButton.OnMouseDown = resetAction

                controls = resetAction.Controls
                controls.Add(textBox)
                controls.Add(checkBox)
                controls.Add(radioButton)
                controls.Add(radioButton2)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace