using System.Diagnostics;
using System.Drawing;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddAndDrawImage
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "AddAndDrawImage.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                PdfImage image = pdf.AddImage("Sample data/ammerland.jpg");
                canvas.DrawImage(image, 10, 50);

                using (Bitmap bitmap = new Bitmap("Sample data/pink.png"))
                {
                    PdfImage image2 = pdf.AddImage(bitmap);
                    canvas.DrawImage(image2, 400, 50, 100, 150, -45);
                }

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}