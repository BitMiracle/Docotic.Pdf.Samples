using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ImportFdfData
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "ImportFdfData.pdf";

            using (PdfDocument pdf = new PdfDocument("Sample data/form.pdf"))
            {
                pdf.ImportFdf("Sample data/form.fdf");

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
