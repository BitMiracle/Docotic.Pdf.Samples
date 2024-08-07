Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Class MainWindow
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        textBoxUrl.Text = "https://bitmiracle.com"
        SetConvertingMode(False)
    End Sub

    Private Sub SetConvertingMode(convertingMode As Boolean)
        textBoxUrl.IsEnabled = Not convertingMode
        buttonConvert.IsEnabled = Not convertingMode
        progressBarConverting.Visibility = If(convertingMode, Visibility.Visible, Visibility.Hidden)
    End Sub

    Private Async Sub Button_Click(sender As Object, e As RoutedEventArgs)
        SetConvertingMode(True)

        Try
            Await ConvertUrlToPdfAsync(textBoxUrl.Text, "HtmlToPdfWpf.pdf")
        Catch ex As HtmlConverterException
            MessageBox.Show(ex.Message, "Conversion failed")
            SetConvertingMode(False)
        End Try
    End Sub

    Private Async Function ConvertUrlToPdfAsync(urlString As String, pdfFileName As String) As Task
        ' NOTE:
        ' Without a license, the library won't allow you to create or read PDF documents.
        ' To get a free time-limited license key, use the form on
        ' https://bitmiracle.com/pdf-library/download

        LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

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
