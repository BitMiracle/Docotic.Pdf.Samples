using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

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
            setConvertingMode(false);
        }

        private void setConvertingMode(bool convertingMode)
        {
            textBoxUrl.IsEnabled = !convertingMode;
            buttonConvert.IsEnabled = !convertingMode;
            progressBarConverting.Visibility = convertingMode ? Visibility.Visible : Visibility.Hidden;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            setConvertingMode(true);

            try
            {
                await convertUrlToPdfAsync(textBoxUrl.Text, "HtmlToPdfWpf.pdf");
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
                using var pdf = await converter.CreatePdfAsync(new Uri(urlString));
                pdf.Save(pdfFileName);
            }

            setConvertingMode(false);

            MessageBox.Show(this, $"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pdfFileName) { UseShellExecute = true });
        }
    }
}
