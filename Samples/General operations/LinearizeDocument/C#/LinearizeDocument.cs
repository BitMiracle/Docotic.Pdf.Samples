using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class LinearizeDocument
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "LinearizeDocument.pdf";

            using (PdfDocument pdf = new PdfDocument(@"Sample Data\gmail-cheat-sheet.pdf"))
            {
                pdf.SaveOptions.Linearize = true;

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
