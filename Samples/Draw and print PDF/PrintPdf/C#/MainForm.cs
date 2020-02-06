using System;
using System.Windows.Forms;
using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Samples.PrintPdf
{
    public partial class MainForm : Form
    {
        private const int FitPageIndex = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private PrintSize getPrintSize()
        {
            return (printSize.SelectedIndex == FitPageIndex) ? PrintSize.FitPage : PrintSize.ActualSize;
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            processExistingPdfDocument(PdfPrintHelper.ShowPrintDialog);
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            processExistingPdfDocument(PdfPrintHelper.ShowPrintPreview);
        }

        private void processExistingPdfDocument(Func<PdfDocument, PrintSize, DialogResult> action)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "PDF files (*.pdf)|*.pdf";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // NOTE: 
                    // When used in trial mode, the library imposes some restrictions.
                    // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
                    // for more information.

                    using (PdfDocument pdf = new PdfDocument(dlg.FileName))
                        action(pdf, getPrintSize());
                }
            }
        }
    }
}
