using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class PDFA
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "PDFA.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                var saveOptions = new PdfSaveOptions { ProducePdfA = true };
                pdf.Save(pathToFile, saveOptions);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
