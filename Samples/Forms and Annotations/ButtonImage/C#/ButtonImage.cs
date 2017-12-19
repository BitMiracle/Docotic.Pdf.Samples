using System.Diagnostics;

using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ButtonImage
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "ButtonImage.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                PdfButton button = page.AddButton(10, 50, 100, 100);
                button.Image = pdf.AddImage(@"Sample Data\\pink.png");
                button.Layout = PdfButtonLayout.ImageOnly;

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
