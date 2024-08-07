Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class HideAction
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "HideAction.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Dim hideeButton As PdfButton = page.AddButton(45, 85, 70, 25)
                hideeButton.Text = "Button"

                Dim hideeTextBox As PdfTextBox = page.AddTextBox(45, 120, 70, 25)
                hideeTextBox.Text = "TextBox"

                Dim hideAction As PdfHideAction = pdf.CreateHideAction(True)
                hideAction.AddControl(hideeButton)
                hideAction.AddControl(hideeTextBox)

                Dim showAction As PdfHideAction = pdf.CreateHideAction(False)
                showAction.AddControl(hideeButton)
                showAction.AddControl(hideeTextBox)

                Dim showButton As PdfButton = page.AddButton("showBtn", 10, 50, 70, 25)
                showButton.Text = "Show controls"
                showButton.OnMouseUp = showAction

                Dim hideButton As PdfButton = page.AddButton("hideBtn", 90, 50, 70, 25)
                hideButton.Text = "Hide controls"
                hideButton.OnMouseUp = hideAction

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace