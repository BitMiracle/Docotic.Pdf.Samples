using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemovePaintedImages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            const string PathToFile = "RemovePaintedImages.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\ImageScaleAndRotate.pdf"))
            {
                PdfPage page = pdf.Pages[0];
                page.RemovePaintedImages(
                    image =>
                    {
                        // remove rotated images
                        PdfMatrix m = image.TransformationMatrix;
                        return Math.Abs(m.M12) > 0.001 || Math.Abs(m.M21) > 0.001;
                    }
                );

                pdf.Save(PathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
