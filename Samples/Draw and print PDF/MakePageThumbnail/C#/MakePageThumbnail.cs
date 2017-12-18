using System.Diagnostics;

using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class MakePageThumbnail
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string pathToImage = "MakePageThumbnail.png";

            using (PdfDocument pdf = new PdfDocument(@"Sample Data/jfif3.pdf"))
            {
                PdfDrawOptions options = PdfDrawOptions.CreateFitSize(new PdfSize(200, 200), false);
                options.BackgroundColor = new PdfGrayColor(100);
                pdf.Pages[0].Save(pathToImage, options);
            }

            Process.Start(pathToImage);
        }
    }
}
