using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractImages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
            {
                foreach (PdfImage image in pdf.GetImages())
                {
                    image.Save("ExtractedImage");

                    // Only extract first image in this sample
                    break;
                }
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
