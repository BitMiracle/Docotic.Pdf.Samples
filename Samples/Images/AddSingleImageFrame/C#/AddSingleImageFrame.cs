using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddSingleImageFrame
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "AddSingleImageFrame.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfImageFrames frames = pdf.OpenImage(@"Sample Data\multipage.tif");
                PdfImageFrame secondFrame = frames[1];

                PdfImage image = pdf.AddImage(secondFrame);
                pdf.Pages[0].Canvas.DrawImage(image, 10, 10, 0);

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
