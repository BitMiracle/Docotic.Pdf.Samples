using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

using BitMiracle.Docotic;
using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace HtmlToPdfWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxUrl.Text = "https://bitmiracle.com";
            SetConvertingMode(false);
        }

        private void SetConvertingMode(bool convertingMode)
        {
            textBoxUrl.Enabled = !convertingMode;
            buttonConvert.Enabled = !convertingMode;
            progressBarConverting.Visible = convertingMode;
        }

        private async void ButtonConvert_Click(object sender, EventArgs e)
        {
            SetConvertingMode(true);

            try
            {
                await ConvertUrlToPdfAsync(textBoxUrl.Text, "HtmlToPdfWindowsForms.pdf");
            }
            catch (HtmlConverterException ex)
            {
                MessageBox.Show(ex.Message, "Conversion failed");
                SetConvertingMode(false);
            }
        }

        private async Task ConvertUrlToPdfAsync(string urlString, string pdfFileName)
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            using (var converter = await HtmlConverter.CreateAsync())
            {
                using var pdf = await converter.CreatePdfAsync(new Uri(urlString));
                pdf.Save(pdfFileName);
            }

            SetConvertingMode(false);

            MessageBox.Show(this, $"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pdfFileName) { UseShellExecute = true });
        }
    }
}
