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
                // Without a license, the library won't allow you to create or read PDF documents.
                // To get a free time-limited license key, use the form on
                // https://bitmiracle.com/pdf-library/download

                LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

                using var pdf = new PdfDocument(dlg.FileName);
                action(pdf, GetPrintSize());
            }
        }
    }
}
