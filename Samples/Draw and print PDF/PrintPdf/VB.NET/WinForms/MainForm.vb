Imports System.Windows.Forms
Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Partial Public Class MainForm
        Inherits Form

        Private Const FitPageIndex = 0

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Function GetPrintSize() As PrintSize
            Return If(printSize.SelectedIndex = FitPageIndex, Samples.PrintSize.FitPage, Samples.PrintSize.ActualSize)
        End Function

        Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles printButton.Click
            ProcessExistingPdfDocument(New Func(Of PdfDocument, PrintSize, DialogResult)(AddressOf ShowPrintDialog))
        End Sub

        Private Sub PreviewButton_Click(sender As Object, e As EventArgs) Handles previewButton.Click
            ProcessExistingPdfDocument(New Func(Of PdfDocument, PrintSize, DialogResult)(AddressOf ShowPrintPreview))
        End Sub

        Private Sub ProcessExistingPdfDocument(action As Func(Of PdfDocument, PrintSize, DialogResult))
            Using dlg As New OpenFileDialog()
                dlg.Filter = "PDF files (*.pdf)|*.pdf"

                If dlg.ShowDialog() = DialogResult.OK Then
                    ' NOTE:
                    ' Without a license, the library won't allow you to create or read PDF documents.
                    ' To get a free time-limited license key, use the form on
                    ' https://bitmiracle.com/pdf-library/download

                    LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

                    Using pdf As PdfDocument = New PdfDocument(dlg.FileName)
                        action(pdf, GetPrintSize())
                    End Using
                End If
            End Using
        End Sub
    End Class
End Namespace
