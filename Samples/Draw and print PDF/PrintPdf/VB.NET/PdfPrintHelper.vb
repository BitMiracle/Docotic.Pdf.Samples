Imports System
Imports System.Windows.Forms
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Friend Module PdfPrintHelper
        Function ShowPrintDialog(ByVal pdf As PdfDocument, ByVal printSize As PrintSize) As DialogResult
            Using printDialog = New PrintDialog()
                printDialog.AllowSomePages = True
                printDialog.AllowCurrentPage = True
                printDialog.AllowSelection = True
                printDialog.PrinterSettings.MinimumPage = 1
                printDialog.PrinterSettings.MaximumPage = pdf.PageCount
                printDialog.PrinterSettings.FromPage = printDialog.PrinterSettings.MinimumPage
                printDialog.PrinterSettings.ToPage = printDialog.PrinterSettings.MaximumPage

                Dim result = printDialog.ShowDialog()
                If result = DialogResult.OK Then
                    Using printDocument = New PdfPrintDocument(pdf, printSize)
                        printDocument.Print(printDialog.PrinterSettings)
                    End Using
                End If

                Return result
            End Using
        End Function

        Function ShowPrintPreview(ByVal pdf As PdfDocument, ByVal printSize As PrintSize) As DialogResult
            Using previewDialog = New PrintPreviewDialog()

                Using printDocument = New PdfPrintDocument(pdf, printSize)
                    previewDialog.Document = printDocument.PrintDocument

                    ' By default the print button sends the preview to the default printer
                    ' The following method replaces the default button with the custom button.
                    ' The custom button opens print dialog.
                    PdfPrintHelper.setupPrintButton(previewDialog, pdf, printSize)

                    ' Remove the following line if you do not want preview maximized
                    previewDialog.WindowState = FormWindowState.Maximized

                    Return previewDialog.ShowDialog()
                End Using
            End Using
        End Function

        Private Sub setupPrintButton(ByVal previewDialog As PrintPreviewDialog, ByVal pdf As PdfDocument, ByVal printSize As PrintSize)
            ' reuse the image of the default print button
            Dim openPrintDialog As ToolStripButton = New ToolStripButton With {
                .Image = CType(previewDialog.Controls(1), ToolStrip).ImageList.Images(0),
                .DisplayStyle = ToolStripItemDisplayStyle.Image
            }

            AddHandler openPrintDialog.Click, New EventHandler(
                Sub(ByVal sender As Object, ByVal e As EventArgs)
                    If PdfPrintHelper.ShowPrintDialog(pdf, printSize) = DialogResult.OK Then
                        previewDialog.Close()
                    End If
                End Sub)

            CType(previewDialog.Controls(1), ToolStrip).Items.RemoveAt(0)
            CType(previewDialog.Controls(1), ToolStrip).Items.Insert(0, openPrintDialog)
        End Sub
    End Module
End Namespace
