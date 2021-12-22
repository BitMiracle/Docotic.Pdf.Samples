using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class DrawZoomedPage
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string pathToImage = "DrawZoomedPage.png";

            using (PdfDocument pdf = new PdfDocument(@"..\Sample Data\jfif3.pdf"))
            {
                PdfDrawOptions options = PdfDrawOptions.CreateZoom(400);
                pdf.Pages[0].Save(pathToImage, options);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
