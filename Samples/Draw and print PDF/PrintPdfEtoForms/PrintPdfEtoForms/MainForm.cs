using System;
using Eto.Forms;
using Eto.Drawing;

namespace BitMiracle.Docotic.Pdf.Samples.PrintPdfEtoForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Title = "Print PDF";
            ClientSize = new Size(400, 350);

            Content = new StackLayout
            {
                Padding = 10,
                Items =
                {
                    new Button(OnPrint)
                    {
                        Text = "Print PDF document"
                    }
                }
            };
        }

        private void OnPrint(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog();
            dlg.Filters.Add("PDF files (*.pdf)|*.pdf");

            if (dlg.ShowDialog(this) == DialogResult.Ok)
            {
                // NOTE:
                // Without a license, the library won't allow you to create or read PDF documents.
                // To get a free time-limited license key, use the form on
                // https://bitmiracle.com/pdf-library/download

                LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

                using var pdf = new PdfDocument(dlg.FileName);
                PdfPrintHelper.ShowPrintDialog(this, pdf);
            }
        }
    }
}
