using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ReplaceImage
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (var pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
            {
                foreach (var image in pdf.GetImages(false))
                    image.ReplaceWith(@"..\Sample Data\ammerland.jpg");

                pdf.Save("ReplaceImage.pdf");
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
