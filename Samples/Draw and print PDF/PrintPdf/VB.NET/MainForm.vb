Imports System
Imports System.Windows.Forms
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Samples.PrintPdf
    Public Partial Class MainForm
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub printButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            processExistingPdfDocument(New Action(Of PdfDocument)(AddressOf ShowPrintDialog))
        End Sub

        Private Sub previewButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            processExistingPdfDocument(New Action(Of PdfDocument)(AddressOf ShowPrintPreview))
        End Sub

        Private Sub processExistingPdfDocument(ByVal action As Action(Of PdfDocument))
            Using dlg As OpenFileDialog = New OpenFileDialog()
                dlg.Filter = "PDF files (*.pdf)|*.pdf"

                If dlg.ShowDialog() = DialogResult.OK Then

                    Using pdf As PdfDocument = New PdfDocument(dlg.FileName)
                        action(pdf)
                    End Using
                End If
            End Using
        End Sub
    End Class
End Namespace
