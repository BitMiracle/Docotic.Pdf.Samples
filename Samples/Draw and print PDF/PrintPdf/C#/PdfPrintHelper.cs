using System.Windows.Forms;
using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Samples.PrintPdf
{
    static class PdfPrintHelper
    {
        public static void ShowPrintDialog(PdfDocument pdf)
        {
            using (PrintDialog printDialog = new PrintDialog())
            {
                printDialog.AllowSomePages = true;
                printDialog.AllowCurrentPage = true;
                printDialog.AllowSelection = true;

                printDialog.PrinterSettings.MinimumPage = 1;
                printDialog.PrinterSettings.MaximumPage = pdf.PageCount;
                printDialog.PrinterSettings.FromPage = printDialog.PrinterSettings.MinimumPage;
                printDialog.PrinterSettings.ToPage = printDialog.PrinterSettings.MaximumPage;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    using (PdfPrintDocument printDocument = new PdfPrintDocument(pdf))
                        printDocument.Print(printDialog.PrinterSettings);
                }
            }
        }

        public static void ShowPrintPreview(PdfDocument pdf)
        {
            using (PrintPreviewDialog previewDialog = new PrintPreviewDialog())
            {
                using (PdfPrintDocument printDocument = new PdfPrintDocument(pdf))
                {
                    previewDialog.Document = printDocument.PrintDocument;
                    previewDialog.ShowDialog();
                }
            }
        }
    }
}
