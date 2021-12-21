using System;
using System.IO;
using System.Reflection;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SavePageAsImage
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToImage = "SavePageAsImage.jpg";

            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using (PdfDocument pdf = new PdfDocument(Path.Combine(location, "jfif3.pdf")))
            {
                PdfDrawOptions options = PdfDrawOptions.Create();
                options.BackgroundColor = new PdfRgbColor(255, 255, 255);
                options.Compression = ImageCompressionOptions.CreateJpeg();

                pdf.Pages[1].Save(pathToImage, options);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
