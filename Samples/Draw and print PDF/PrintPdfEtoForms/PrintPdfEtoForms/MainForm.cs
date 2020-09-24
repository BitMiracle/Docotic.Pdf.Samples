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
                    new Button(onPrint)
                    {
                        Text = "Print PDF document"
                    }
                }
            };
        }

        private void onPrint(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filters.Add("PDF files (*.pdf)|*.pdf");

                if (dlg.ShowDialog(this) == DialogResult.Ok)
                {
                    // NOTE: 
                    // When used in trial mode, the library imposes some restrictions.
                    // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
                    // for more information.

                    using (var pdf = new PdfDocument(dlg.FileName))
                        PdfPrintHelper.ShowPrintDialog(this, pdf);
                }
            }
        }
    }
}
