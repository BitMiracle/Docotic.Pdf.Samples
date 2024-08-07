using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

using BitMiracle.Docotic;
using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace HtmlToPdfWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBoxUrl.Text = "https://bitmiracle.com";
            SetConvertingMode(false);
        }

        private void SetConvertingMode(bool convertingMode)
        {
            textBoxUrl.IsEnabled = !convertingMode;
            buttonConvert.IsEnabled = !convertingMode;
            progressBarConverting.Visibility = convertingMode ? Visibility.Visible : Visibility.Hidden;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SetConvertingMode(true);

            try
            {
                await ConvertUrlToPdfAsync(textBoxUrl.Text, "HtmlToPdfWpf.pdf");
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
