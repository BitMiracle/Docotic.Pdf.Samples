Imports System
Imports System.Windows.Forms
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Samples.PrintPdf
    Partial Public Class MainForm
        Inherits Form

        Private Const FitPageIndex = 0

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Function getPrintSize() As PrintSize
            Return If(printSize.SelectedIndex = FitPageIndex, PrintPdf.PrintSize.FitPage, PrintPdf.PrintSize.ActualSize)
        End Function

        Private Sub printButton_Click(sender As Object, e As EventArgs) Handles printButton.Click
            processExistingPdfDocument(New Func(Of PdfDocument, PrintSize, DialogResult)(AddressOf ShowPrintDialog))
        End Sub

        Private Sub previewButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles previewButton.Click
            processExistingPdfDocument(New Func(Of PdfDocument, PrintSize, DialogResult)(AddressOf ShowPrintPreview))
        End Sub

        Private Sub processExistingPdfDocument(ByVal action As Func(Of PdfDocument, PrintSize, DialogResult))
            Using dlg As OpenFileDialog = New OpenFileDialog()
                dlg.Filter = "PDF files (*.pdf)|*.pdf"

                If dlg.ShowDialog() = DialogResult.OK Then

                    Using pdf As PdfDocument = New PdfDocument(dlg.FileName)
                        action(pdf, getPrintSize())
                    End Using
                End If
            End Using
        End Sub
    End Class
End Namespace
