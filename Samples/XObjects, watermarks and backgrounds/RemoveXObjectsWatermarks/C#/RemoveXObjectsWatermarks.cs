using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemoveXObjectsWatermarks
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.
            
            string pathToFile = "RemoveXObjectsWatermarks.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\DocumentWithWatermark.pdf"))
            {
                foreach (PdfPage page in pdf.Pages)
                {
                    // remove first XObject (in this case it's a watermark)
                    page.XObjects.RemoveAt(0);
                }
                
                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
