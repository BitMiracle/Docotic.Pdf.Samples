﻿Namespace BitMiracle.Docotic.Pdf.Samples
    Class MainWindow
        Private Const FitPageIndex As Integer = 0

        Private Function GetPrintSize() As PrintSize
            Return If(PrintSizeCombobox.SelectedIndex = FitPageIndex, PrintSize.FitPage, PrintSize.ActualSize)
        End Function

        Private Sub Print_Click(sender As Object, e As RoutedEventArgs)
            ProcessExistingPdfDocument(AddressOf PdfPrintHelper.ShowPrintDialog)
        End Sub

        Private Sub Preview_Click(sender As Object, e As RoutedEventArgs)
            ProcessExistingPdfDocument(AddressOf PdfPrintHelper.ShowPrintPreview)
        End Sub

        Private Sub ProcessExistingPdfDocument(action As Func(Of PdfDocument, PrintSize, Forms.DialogResult))
            Using dlg = New Forms.OpenFileDialog()
                dlg.Filter = "PDF files (*.pdf)|*.pdf"

                If dlg.ShowDialog() = Forms.DialogResult.OK Then
                    ' NOTE:
                    ' When used in trial mode, the library imposes some restrictions.
                    ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
                    ' for more information.

                    Using pdf = New PdfDocument(dlg.FileName)
                        action(pdf, GetPrintSize())
                    End Using
                End If
            End Using
        End Sub
    End Class
End Namespace