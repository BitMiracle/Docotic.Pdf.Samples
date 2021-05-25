Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Public Class Form1
    <STAThread()>
    Public Shared Sub Main()
        Application.SetHighDpiMode(HighDpiMode.SystemAware)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Dim form As Form1 = New Form1
        form.setup()

        Application.Run(form)
    End Sub

    Private Sub setup()
        textBoxUrl.Text = "https://bitmiracle.com"
        setConvertingMode(False)
    End Sub

    Private Sub setConvertingMode(convertingMode As Boolean)
        textBoxUrl.Enabled = Not convertingMode
        buttonConvert.Enabled = Not convertingMode
        progressBarConverting.Visible = convertingMode
    End Sub

    Private Async Sub buttonConvert_Click(sender As Object, e As EventArgs) Handles buttonConvert.Click
        setConvertingMode(True)
        Await convertUrlToPdfAsync(textBoxUrl.Text, "HtmlToPdfWindowsForms.pdf")
    End Sub

    Private Async Function convertUrlToPdfAsync(ByVal urlString As String, ByVal pdfFileName As String) As Task
        Using converter = Await HtmlConverter.CreateAsync()

            Using pdf = Await converter.CreatePdfAsync(New Uri(urlString))
                pdf.Save(pdfFileName)
            End Using
        End Using

        setConvertingMode(False)
        MessageBox.Show(Me, $"The output is located in {Environment.CurrentDirectory}")
    End Function
End Class
