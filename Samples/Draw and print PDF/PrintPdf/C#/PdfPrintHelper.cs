using System.Windows.Forms;
using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Samples.PrintPdf
{
    static class PdfPrintHelper
    {
        public static void ShowPrintDialog(PdfDocument pdf, PrintSize printSize)
        {
            using (var printDialog = new PrintDialog())
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
                    using (var printDocument = new PdfPrintDocument(pdf, printSize))
                        printDocument.Print(printDialog.PrinterSettings);
                }
            }
        }

        public static void ShowPrintPreview(PdfDocument pdf, PrintSize printSize)
        {
            using (var previewDialog = new PrintPreviewDialog())
            {
                using (var printDocument = new PdfPrintDocument(pdf, printSize))
                {
                    previewDialog.Document = printDocument.PrintDocument;
                    previewDialog.ShowDialog();
                }
            }
        }
    }
}
