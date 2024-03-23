using System;
using System.Windows.Forms;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public partial class MainForm : Form
    {
        private const int FitPageIndex = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private PrintSize GetPrintSize()
        {
            return (printSize.SelectedIndex == FitPageIndex) ? PrintSize.FitPage : PrintSize.ActualSize;
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            ProcessExistingPdfDocument(PdfPrintHelper.ShowPrintDialog);
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            ProcessExistingPdfDocument(PdfPrintHelper.ShowPrintPreview);
        }

        private void ProcessExistingPdfDocument(Func<PdfDocument, PrintSize, DialogResult> action)
        {
            using var dlg = new OpenFileDialog();
            dlg.Filter = "PDF files (*.pdf)|*.pdf";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // NOTE:
                // When used in trial mode, the library imposes some restrictions.
                // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
                // for more information.

                using var pdf = new PdfDocument(dlg.FileName);
                action(pdf, GetPrintSize());
            }
        }
    }
}
