using BitMiracle.Docotic.Pdf;
using Eto.Forms;

namespace BitMiracle.Docotic.Pdf.Samples.PrintPdfEtoForms
{
    static class PdfPrintHelper
    {
        public static DialogResult ShowPrintDialog(Control parent, PdfDocument pdf)
        {
            using (var printDialog = new PrintDialog())
            {
                printDialog.AllowPageRange = true;
                printDialog.AllowSelection = true;

                // PrintDialog.PrintSettings is null by default on macOS
                if (printDialog.PrintSettings == null)
                    printDialog.PrintSettings = new PrintSettings();

                var range = new Range<int>(1, pdf.PageCount);
                printDialog.PrintSettings.MaximumPageRange = range;
                printDialog.PrintSettings.SelectedPageRange = range;

                var result = printDialog.ShowDialog(parent);
                if (result == DialogResult.Ok)
                {
                    using (var pdfPrint = new PdfPrintDocument(pdf))
                        pdfPrint.Print(printDialog.PrintSettings);
                }

                return result;
            }
        }
    }
}
