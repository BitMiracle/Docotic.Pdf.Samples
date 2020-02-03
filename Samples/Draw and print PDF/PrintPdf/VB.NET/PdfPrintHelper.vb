Imports System.Windows.Forms
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Samples.PrintPdf
    Friend Module PdfPrintHelper
        Sub ShowPrintDialog(ByVal pdf As PdfDocument)
            Using printDialog As PrintDialog = New PrintDialog()
                printDialog.AllowSomePages = True
                printDialog.AllowCurrentPage = True
                printDialog.AllowSelection = True
                printDialog.PrinterSettings.MinimumPage = 1
                printDialog.PrinterSettings.MaximumPage = pdf.PageCount
                printDialog.PrinterSettings.FromPage = printDialog.PrinterSettings.MinimumPage
                printDialog.PrinterSettings.ToPage = printDialog.PrinterSettings.MaximumPage

                If printDialog.ShowDialog() = DialogResult.OK Then

                    Using printDocument As PdfPrintDocument = New PdfPrintDocument(pdf)
                        printDocument.Print(printDialog.PrinterSettings)
                    End Using
                End If
            End Using
        End Sub

        Sub ShowPrintPreview(ByVal pdf As PdfDocument)
            Using previewDialog As PrintPreviewDialog = New PrintPreviewDialog()

                Using printDocument As PdfPrintDocument = New PdfPrintDocument(pdf)
                    previewDialog.Document = printDocument.PrintDocument
                    previewDialog.ShowDialog()
                End Using
            End Using
        End Sub
    End Module
End Namespace
