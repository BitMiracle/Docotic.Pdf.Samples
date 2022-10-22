using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace HtmlToPdfWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxUrl.Text = "https://bitmiracle.com";
            setConvertingMode(false);
        }

        private void setConvertingMode(bool convertingMode)
        {
            textBoxUrl.Enabled = !convertingMode;
            buttonConvert.Enabled = !convertingMode;
            progressBarConverting.Visible = convertingMode;
        }

        private async void buttonConvert_Click(object sender, EventArgs e)
        {
            setConvertingMode(true);

            try
            {
                await convertUrlToPdfAsync(textBoxUrl.Text, "HtmlToPdfWindowsForms.pdf");
            }
            catch (HtmlConverterException ex)
            {
                MessageBox.Show(ex.Message, "Conversion failed");
                setConvertingMode(false);
            }
        }

        private async Task convertUrlToPdfAsync(string urlString, string pdfFileName)
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (var converter = await HtmlConverter.CreateAsync())
            {
                using (var pdf = await converter.CreatePdfAsync(new Uri(urlString)))
                    pdf.Save(pdfFileName);
            }

            setConvertingMode(false);

            MessageBox.Show(this, $"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pdfFileName) { UseShellExecute = true });
        }
    }
}
