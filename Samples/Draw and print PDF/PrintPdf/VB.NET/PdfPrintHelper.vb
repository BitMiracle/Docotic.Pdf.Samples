Imports System.Windows.Forms
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Samples.PrintPdf
    Friend Module PdfPrintHelper
        Sub ShowPrintDialog(ByVal pdf As PdfDocument, ByVal printSize As PrintSize)
            Using printDialog = New PrintDialog()
                printDialog.AllowSomePages = True
                printDialog.AllowCurrentPage = True
                printDialog.AllowSelection = True
                printDialog.PrinterSettings.MinimumPage = 1
                printDialog.PrinterSettings.MaximumPage = pdf.PageCount
                printDialog.PrinterSettings.FromPage = printDialog.PrinterSettings.MinimumPage
                printDialog.PrinterSettings.ToPage = printDialog.PrinterSettings.MaximumPage

                If printDialog.ShowDialog() = DialogResult.OK Then

                    Using printDocument = New PdfPrintDocument(pdf, printSize)
                        printDocument.Print(printDialog.PrinterSettings)
                    End Using
                End If
            End Using
        End Sub

        Sub ShowPrintPreview(ByVal pdf As PdfDocument, ByVal printSize As PrintSize)
            Using previewDialog = New PrintPreviewDialog()

                Using printDocument = New PdfPrintDocument(pdf, printSize)
                    previewDialog.Document = printDocument.PrintDocument
                    previewDialog.ShowDialog()
                End Using
            End Using
        End Sub
    End Module
End Namespace
