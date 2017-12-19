using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Buttons
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "Buttons.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                PdfButton button = page.AddButton(10, 50, 100, 50);
                button.BackgroundColor = new PdfGrayColor(60);
                button.BorderColor = new PdfRgbColor(128, 0, 128);
                button.Text = "Button";

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
