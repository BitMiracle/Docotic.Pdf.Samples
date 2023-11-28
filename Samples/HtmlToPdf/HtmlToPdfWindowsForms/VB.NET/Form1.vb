Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Public Class Form1
    <STAThread()>
    Public Shared Sub Main()
        Application.SetHighDpiMode(HighDpiMode.SystemAware)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Dim form As New Form1
        form.Setup()

        Application.Run(form)
    End Sub

    Private Sub Setup()
        textBoxUrl.Text = "https://bitmiracle.com"
        SetConvertingMode(False)
    End Sub

    Private Sub SetConvertingMode(convertingMode As Boolean)
        textBoxUrl.Enabled = Not convertingMode
        buttonConvert.Enabled = Not convertingMode
        progressBarConverting.Visible = convertingMode
    End Sub

    Private Async Sub ButtonConvert_Click(sender As Object, e As EventArgs) Handles buttonConvert.Click
        SetConvertingMode(True)

        Try
            Await ConvertUrlToPdfAsync(textBoxUrl.Text, "HtmlToPdfWindowsForms.pdf")
        Catch ex As HtmlConverterException
            MessageBox.Show(ex.Message, "Conversion failed")
            SetConvertingMode(False)
        End Try
    End Sub

    Private Async Function ConvertUrlToPdfAsync(urlString As String, pdfFileName As String) As Task
        Using converter = Await HtmlConverter.CreateAsync()

            Using pdf = Await converter.CreatePdfAsync(New Uri(urlString))
                pdf.Save(pdfFileName)
            End Using
        End Using

        SetConvertingMode(False)
        MessageBox.Show(Me, $"The output is located in {Environment.CurrentDirectory}")

        Process.Start(New ProcessStartInfo(pdfFileName) With {.UseShellExecute = True})
    End Function
End Class
